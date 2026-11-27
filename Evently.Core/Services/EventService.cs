
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class EventService
    {

        public static DataTable GetEvent()
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT * FROM events;";
            MySqlDataAdapter adapter = new(query, conn);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        // returns the new event_id, or 0 when the insert was refused
        public static int AddEvent(string name, string venue, string description, DateTime date, int capacity, DateTime? registrationDeadline)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"INSERT INTO events(name, venue, description, event_date, capacity, registration_deadline)
                             VALUES
                             (@name, @venue, @description, @event_date, @capacity, @registration_deadline);
                             SELECT LAST_INSERT_ID();"; // retrieves the last id
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@venue", venue);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@event_date", date);
            cmd.Parameters.AddWithValue("@capacity", capacity);
            cmd.Parameters.AddWithValue("@registration_deadline", DeadlineParameterValue(registrationDeadline));

            try
            {
                int newEventId = Convert.ToInt32(cmd.ExecuteScalar());

                if (newEventId > 0)
                {
                    ActivityLogService.Add("events", $"Added event '{name}'.");
                }

                return newEventId;
            }
            catch (MySqlException ex)
            {
                // 3819 = a CHECK rule refused the row, here capacity > 0
                if (ex.Number == 3819) { return 0; }
                // 1406 = a string is longer than its column, e.g. name over 100 chars
                if (ex.Number == 1406) { return 0; }

                // anything else is not the user's fault: wrong db.config, MySQL down
                // rethrow so the form shows the real message instead of a guess
                throw;
            }

        }

        public static bool DeleteEvent(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();

            // read the name first, because the row is gone after the delete
            string nameQuery = "SELECT name FROM events WHERE event_id = @eventId";
            using MySqlCommand nameCmd = new(nameQuery, conn);
            nameCmd.Parameters.AddWithValue("@eventId", eventId);
            string eventName = Convert.ToString(nameCmd.ExecuteScalar()) ?? "";

            string query = "DELETE FROM events WHERE event_id = @eventId";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            bool deleted = cmd.ExecuteNonQuery() > 0;

            if (deleted)
            {
                ActivityLogService.Add("events", $"Deleted event '{eventName}'.");
            }

            return deleted;
        }

        public static bool UpdateEvent(int eventId, string name, string venue, string description, DateTime date, int capacity, DateTime? registrationDeadline)
        {
            using var conn = DatabaseConnection.GetConnection();

            // 0 changed rows can mean "nothing to change", so check the row exists first
            string checkQuery = "SELECT COUNT(*) FROM events WHERE event_id = @eventId";
            using MySqlCommand check = new(checkQuery, conn);
            check.Parameters.AddWithValue("@eventId", eventId);
            if (Convert.ToInt32(check.ExecuteScalar()) == 0) { return false; } // deleted meanwhile

            string query = @"UPDATE events
                             SET name = @name,
                                 venue = @venue,
                                 description = @description,
                                 event_date = @event_date,
                                 capacity = @capacity,
                                 registration_deadline = @registration_deadline
                             WHERE event_id = @eventId";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@venue", venue);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@event_date", date);
            cmd.Parameters.AddWithValue("@capacity", capacity);
            cmd.Parameters.AddWithValue("@registration_deadline", DeadlineParameterValue(registrationDeadline));
            cmd.Parameters.AddWithValue("@eventId", eventId);

            try
            {
                cmd.ExecuteNonQuery();
                ActivityLogService.Add("events", $"Edited event '{name}'.");
                return true; // the row exists, and an identical save is still a success
            }
            catch (MySqlException ex)
            {
                // 3819 = CHECK failed, so capacity was 0 or less
                if (ex.Number == 3819) { return false; }
                // 1406 = a string is longer than its column
                if (ex.Number == 1406) { return false; }
                throw;
            }
        }

        // earlier of the deadline and the event start; no deadline means the event start
        public static bool IsRegistrationOpen(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT LEAST(COALESCE(registration_deadline, event_date), event_date)
                             FROM events WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            object? result = cmd.ExecuteScalar();

            if (result == null) { return false; } // no such event

            DateTime cutoff = Convert.ToDateTime(result);
            return DateTime.Now < cutoff;
        }

        public static int GetRemainingCapacity(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT GREATEST(0, e.capacity - COUNT(t.ticket_id))
                             FROM events e
                             LEFT JOIN tickets t ON e.event_id = t.event_id
                             WHERE e.event_id = @eventId
                             GROUP BY e.event_id, e.capacity;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // an empty deadline is stored as NULL, not as year 0001
        private static object DeadlineParameterValue(DateTime? registrationDeadline)
        {
            if (registrationDeadline.HasValue)
            {
                return registrationDeadline.Value;
            }

            return DBNull.Value;
        }
    }
}