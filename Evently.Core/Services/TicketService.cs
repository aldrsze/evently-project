using System;
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class TicketService
    {
        public static DataTable GetAttendeesByEvent(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                t.ticket_id,
                                a.attendee_id,
                                a.first_name,
                                a.last_name,
                                CONCAT(a.first_name, ' ', a.last_name) AS name,
                                a.birthdate,
                                CASE 
                                    WHEN a.birthdate IS NOT NULL THEN TIMESTAMPDIFF(YEAR, a.birthdate, CURDATE())
                                    ELSE NULL
                                END AS age,
                                a.email,
                                CASE WHEN t.checked_in_at IS NOT NULL THEN 'Yes' ELSE 'No' END AS is_checked_in,
                                CASE WHEN t.ticket_emailed = TRUE THEN 'Yes' ELSE 'No' END AS ticket_emailed,
                                CASE WHEN t.review_emailed = TRUE THEN 'Yes' ELSE 'No' END AS review_emailed
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             WHERE t.event_id = @eventId;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static bool AddAttendee(int eventId, string firstName, string lastName, string email, DateTime birthdate)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            email = email.Trim();

            // no manager means no registration, from any of the three entry points
            if (!OrganizerService.EventHasManager(eventId))
            {
                return false;
            }

            // a passed cutoff (deadline or event start) closes registration everywhere
            if (!EventService.IsRegistrationOpen(eventId))
            {
                return false;
            }

            using var conn = DatabaseConnection.GetConnection();

            int attendeeId = 0;
            string findQuery = "SELECT attendee_id FROM attendees WHERE email = @email;";
            using (MySqlCommand findCmd = new(findQuery, conn))
            {
                findCmd.Parameters.AddWithValue("@email", email);
                object? result = findCmd.ExecuteScalar();
                if (result != null)
                {
                    attendeeId = Convert.ToInt32(result);
                }
            }

            if (attendeeId == 0)
            {
                string insertAttendeeQuery = @"INSERT INTO attendees(first_name, last_name, email, birthdate)
                                               VALUES (@firstName, @lastName, @email, @birthdate);
                                               SELECT LAST_INSERT_ID();";
                using MySqlCommand insertCmd = new(insertAttendeeQuery, conn);
                insertCmd.Parameters.AddWithValue("@firstName", firstName);
                insertCmd.Parameters.AddWithValue("@lastName", lastName);
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.Parameters.AddWithValue("@birthdate", birthdate);

                try
                {
                    attendeeId = Convert.ToInt32(insertCmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) { return false; }
                    if (ex.Number == 1406) { return false; }
                    throw;
                }
            }

            string insertTicketQuery = @"INSERT INTO tickets(ticket_id, attendee_id, event_id)
                                         VALUES (@ticketId, @attendeeId, @eventId);";
            using MySqlCommand ticketCmd = new(insertTicketQuery, conn);
            ticketCmd.Parameters.AddWithValue("@ticketId", Guid.NewGuid().ToString());
            ticketCmd.Parameters.AddWithValue("@attendeeId", attendeeId);
            ticketCmd.Parameters.AddWithValue("@eventId", eventId);

            try
            {
                bool added = ticketCmd.ExecuteNonQuery() > 0;

                if (added)
                {
                    string eventName = GetEventName(eventId);
                    ActivityLogService.Add("attendees", $"Added attendee '{firstName} {lastName}' to event '{eventName}'.");
                }

                return added;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; }
                throw;
            }
        }

        public static bool DeleteAttendee(int eventId, int attendeeId)
        {
            using var conn = DatabaseConnection.GetConnection();

            // read the name first, because the attendee row may be deleted below
            string nameQuery = "SELECT CONCAT(first_name, ' ', last_name) FROM attendees WHERE attendee_id = @attendeeId;";
            using MySqlCommand nameCmd = new(nameQuery, conn);
            nameCmd.Parameters.AddWithValue("@attendeeId", attendeeId);
            string attendeeName = Convert.ToString(nameCmd.ExecuteScalar()) ?? "";

            string deleteTicketQuery = "DELETE FROM tickets WHERE event_id = @eventId AND attendee_id = @attendeeId;";
            using (MySqlCommand ticketCmd = new(deleteTicketQuery, conn))
            {
                ticketCmd.Parameters.AddWithValue("@eventId", eventId);
                ticketCmd.Parameters.AddWithValue("@attendeeId", attendeeId);
                if (ticketCmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
            }

            // If attendee has no tickets left in any event, remove attendee record
            string countQuery = "SELECT COUNT(*) FROM tickets WHERE attendee_id = @attendeeId;";
            using MySqlCommand countCmd = new(countQuery, conn);
            countCmd.Parameters.AddWithValue("@attendeeId", attendeeId);
            int remaining = Convert.ToInt32(countCmd.ExecuteScalar());
            if (remaining == 0)
            {
                string deleteAttendeeQuery = "DELETE FROM attendees WHERE attendee_id = @attendeeId;";
                using MySqlCommand attendeeCmd = new(deleteAttendeeQuery, conn);
                attendeeCmd.Parameters.AddWithValue("@attendeeId", attendeeId);
                attendeeCmd.ExecuteNonQuery();
            }

            string logName = attendeeName == "" ? "attendee" : attendeeName;
            string eventName = GetEventName(eventId);
            ActivityLogService.Add("attendees", $"Removed attendee '{logName}' from event '{eventName}'.");

            return true;
        }

        public static bool UpdateAttendee(int eventId, int attendeeId, string firstName, string lastName, string email, DateTime birthdate)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            email = email.Trim();

            using var conn = DatabaseConnection.GetConnection();

            string checkQuery = "SELECT COUNT(*) FROM attendees WHERE attendee_id = @attendeeId;";
            using (MySqlCommand check = new(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@attendeeId", attendeeId);
                if (Convert.ToInt32(check.ExecuteScalar()) == 0) { return false; }
            }

            string query = @"UPDATE attendees
                             SET first_name = @firstName,
                                 last_name = @lastName,
                                 email = @email,
                                 birthdate = @birthdate
                             WHERE attendee_id = @attendeeId;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@birthdate", birthdate);
            cmd.Parameters.AddWithValue("@attendeeId", attendeeId);

            try
            {
                cmd.ExecuteNonQuery();
                string eventName = GetEventName(eventId);
                ActivityLogService.Add("attendees", $"Edited attendee '{firstName} {lastName}' in event '{eventName}'.");
                return true;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; }
                if (ex.Number == 1406) { return false; }
                throw;
            }
        }

        public static DataTable GetUnemailedTicketsByEvent(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                t.ticket_id,
                                a.email,
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                e.name AS event_name,
                                e.venue,
                                e.event_date
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             INNER JOIN events e ON t.event_id = e.event_id
                             WHERE t.event_id = @eventId AND t.ticket_emailed = FALSE;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataRow? GetTicketDetails(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                t.ticket_id,
                                t.ticket_emailed,
                                t.checked_in_at,
                                t.checked_out_at,
                                a.email,
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                e.name AS event_name,
                                e.venue,
                                e.event_date
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             INNER JOIN events e ON t.event_id = e.event_id
                             WHERE t.ticket_id = @ticketId;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public static bool CheckInTicket(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "UPDATE tickets SET checked_in_at = NOW() WHERE ticket_id = @ticketId AND checked_in_at IS NULL;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool CheckOutTicket(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "UPDATE tickets SET checked_out_at = NOW() WHERE ticket_id = @ticketId AND checked_in_at IS NOT NULL AND checked_out_at IS NULL;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool MarkTicketAsEmailed(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "UPDATE tickets SET ticket_emailed = TRUE WHERE ticket_id = @ticketId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            return cmd.ExecuteNonQuery() > 0;
        }

        // the log message names the event, so an attendee line means something outside this screen
        private static string GetEventName(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT name FROM events WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            return Convert.ToString(cmd.ExecuteScalar()) ?? "";
        }
    }
}
