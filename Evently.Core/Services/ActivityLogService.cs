using System;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    // Every add, edit, or delete writes one row. Panels read only the newest row.
    public static class ActivityLogService
    {
        public static void Add(string entity, string message)
        {
            if (message.Length > 255)
            {
                message = message[..255];
            }

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                string query = @"INSERT INTO activity_log(entity, message, created_at)
                                 VALUES (@entity, @message, NOW());";
                using MySqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@entity", entity);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                // An old database without the table must not break the real change.
            }
        }

        // Returns "" when this entity has no rows yet.
        public static string GetLatest(string entity)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                string query = @"SELECT message
                                 FROM activity_log
                                 WHERE entity = @entity
                                 ORDER BY log_id DESC
                                 LIMIT 1;";
                using MySqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@entity", entity);
                object? result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    return "";
                }

                return Convert.ToString(result) ?? "";
            }
            catch (MySqlException)
            {
                return "";
            }
        }
    }
}
