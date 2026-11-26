using System;
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class OrganizerService
    {
        // an event with no manager cannot take registrations
        public static bool EventHasManager(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT COUNT(*) FROM event_organizers WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public static DataTable GetAllOrganizers()
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT organizer_id, full_name, email, phone, organization
                             FROM organizers
                             ORDER BY full_name;";
            using MySqlCommand cmd = new(query, conn);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetAvailableOrganizers(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT organizer_id, full_name, email, phone, organization
                             FROM organizers
                             WHERE organizer_id NOT IN (
                                 SELECT organizer_id FROM event_organizers WHERE event_id = @eventId
                             )
                             ORDER BY full_name;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetOrganizersByEvent(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                o.organizer_id,
                                o.full_name,
                                o.email,
                                o.phone,
                                o.organization
                             FROM organizers o
                             INNER JOIN event_organizers eo ON o.organizer_id = eo.organizer_id
                             WHERE eo.event_id = @eventId
                             ORDER BY o.full_name;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static bool LinkOrganizerToEvent(int eventId, int organizerId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string linkQuery = @"INSERT INTO event_organizers(event_id, organizer_id)
                                 VALUES (@eventId, @organizerId);";
            using MySqlCommand linkCmd = new(linkQuery, conn);
            linkCmd.Parameters.AddWithValue("@eventId", eventId);
            linkCmd.Parameters.AddWithValue("@organizerId", organizerId);

            try
            {
                return linkCmd.ExecuteNonQuery() > 0;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; }
                throw;
            }
        }

        public static bool AddOrganizerToEvent(int eventId, string fullName, string email, string phone, string organization)
        {
            fullName = fullName.Trim();
            email = email.Trim();
            phone = phone.Trim();
            organization = organization.Trim();

            using var conn = DatabaseConnection.GetConnection();

            int organizerId = 0;
            string checkQuery = "SELECT organizer_id FROM organizers WHERE email = @email;";
            using (MySqlCommand checkCmd = new(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@email", email);
                object? result = checkCmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    organizerId = Convert.ToInt32(result);
                }
            }

            if (organizerId == 0)
            {
                string insertQuery = @"INSERT INTO organizers(full_name, email, phone, organization)
                                       VALUES (@fullName, @email, @phone, @organization);
                                       SELECT LAST_INSERT_ID();";
                using MySqlCommand insertCmd = new(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@fullName", fullName);
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phone) ? DBNull.Value : phone);
                insertCmd.Parameters.AddWithValue("@organization", string.IsNullOrEmpty(organization) ? DBNull.Value : organization);

                try
                {
                    organizerId = Convert.ToInt32(insertCmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) { return false; }
                    throw;
                }
            }

            bool linked = LinkOrganizerToEvent(eventId, organizerId);

            if (linked)
            {
                string eventName = GetEventName(eventId);
                ActivityLogService.Add("organizers", $"Added manager '{fullName}' to event '{eventName}'.");
            }

            return linked;
        }

        // the log message names the event, so a manager line means something outside this screen
        private static string GetEventName(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT name FROM events WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            return Convert.ToString(cmd.ExecuteScalar()) ?? "";
        }

        public static bool RemoveOrganizerFromEvent(int eventId, int organizerId)
        {
            using var conn = DatabaseConnection.GetConnection();

            // read the name first, because the row can be deleted below
            string nameQuery = "SELECT full_name FROM organizers WHERE organizer_id = @organizerId;";
            using MySqlCommand nameCmd = new(nameQuery, conn);
            nameCmd.Parameters.AddWithValue("@organizerId", organizerId);
            string managerName = Convert.ToString(nameCmd.ExecuteScalar()) ?? "";

            string unlinkQuery = "DELETE FROM event_organizers WHERE event_id = @eventId AND organizer_id = @organizerId;";
            using (MySqlCommand unlinkCmd = new(unlinkQuery, conn))
            {
                unlinkCmd.Parameters.AddWithValue("@eventId", eventId);
                unlinkCmd.Parameters.AddWithValue("@organizerId", organizerId);

                if (unlinkCmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
            }

            // the manager row stays only while another event still uses it
            string cleanupQuery = @"DELETE FROM organizers
                                    WHERE organizer_id = @organizerId
                                      AND NOT EXISTS (SELECT 1 FROM event_organizers eo
                                                      WHERE eo.organizer_id = @organizerId);";
            using MySqlCommand cleanupCmd = new(cleanupQuery, conn);
            cleanupCmd.Parameters.AddWithValue("@organizerId", organizerId);
            cleanupCmd.ExecuteNonQuery();

            string logName = managerName == "" ? "manager" : managerName;
            string eventName = GetEventName(eventId);
            ActivityLogService.Add("organizers", $"Removed manager '{logName}' from event '{eventName}'.");

            return true;
        }

        public static bool UpdateOrganizer(int organizerId, string fullName, string email, string phone, string organization)
        {
            fullName = fullName.Trim();
            email = email.Trim();
            phone = phone.Trim();
            organization = organization.Trim();

            using var conn = DatabaseConnection.GetConnection();

            string checkQuery = "SELECT COUNT(*) FROM organizers WHERE organizer_id = @organizerId;";
            using (MySqlCommand check = new(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@organizerId", organizerId);
                if (Convert.ToInt32(check.ExecuteScalar()) == 0) { return false; }
            }

            string query = @"UPDATE organizers 
                             SET full_name = @fullName,
                                 email = @email,
                                 phone = @phone,
                                 organization = @organization
                             WHERE organizer_id = @organizerId;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@fullName", fullName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phone) ? DBNull.Value : phone);
            cmd.Parameters.AddWithValue("@organization", string.IsNullOrEmpty(organization) ? DBNull.Value : organization);
            cmd.Parameters.AddWithValue("@organizerId", organizerId);

            try
            {
                cmd.ExecuteNonQuery();
                ActivityLogService.Add("organizers", $"Edited manager '{fullName}'.");
                return true;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; }
                throw;
            }
        }
    }
}
