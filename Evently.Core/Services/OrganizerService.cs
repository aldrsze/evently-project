using System;
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class OrganizerService
    {
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

        public static bool AddOrganizerToEvent(int eventId, string fullName, string email, string phone, string organization)
        {
            fullName = fullName.Trim();
            email = email.Trim();
            phone = phone.Trim();
            organization = organization.Trim();

            using var conn = DatabaseConnection.GetConnection();

            int organizerId = 0;
            string findQuery = "SELECT organizer_id FROM organizers WHERE email = @email;";
            using (MySqlCommand findCmd = new(findQuery, conn))
            {
                findCmd.Parameters.AddWithValue("@email", email);
                object? res = findCmd.ExecuteScalar();
                if (res != null)
                {
                    organizerId = Convert.ToInt32(res);
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
                if (ex.Number == 1062) { return false; } // already linked
                throw;
            }
        }

        public static bool RemoveOrganizerFromEvent(int eventId, int organizerId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "DELETE FROM event_organizers WHERE event_id = @eventId AND organizer_id = @organizerId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            cmd.Parameters.AddWithValue("@organizerId", organizerId);

            return cmd.ExecuteNonQuery() > 0;
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
