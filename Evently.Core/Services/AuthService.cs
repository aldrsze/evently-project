using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class AuthService
    {
        public static bool VerifyAuth(string identifier, string password)
        {
            string? storedHash = GetPasswordHash(identifier);

            if (storedHash == null)
            {
                return false;
            }

            return PasswordHelper.VerifyPassword(password, storedHash);
        }

        public static string? GetPasswordHash(string identifier)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT password FROM users WHERE (username = @identifier OR email = @identifier) AND is_active = 1";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@identifier", identifier);
            return cmd.ExecuteScalar()?.ToString();
        }

        public static string? GetUsername(string identifier)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT username FROM users WHERE username = @identifier OR email = @identifier";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@identifier", identifier);
            return cmd.ExecuteScalar()?.ToString();
        }

        public static string? GetRole(string identifier)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT role FROM users WHERE username = @identifier OR email = @identifier";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@identifier", identifier);
            return cmd.ExecuteScalar()?.ToString();
        }

        // a staff signup cannot sign in until an admin approves it
        public static bool IsApproved(string identifier)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT is_approved FROM users WHERE (username = @identifier OR email = @identifier) AND is_active = 1";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@identifier", identifier);
            object? result = cmd.ExecuteScalar();

            if (result == null)
            {
                return false;
            }

            return Convert.ToBoolean(result);
        }
    }
}