using System.Data;
using MySqlConnector;
using Evently.Core.Helpers;

namespace Evently.Core.Services
{
    public static class UserService
    {
        public const int MinPasswordLength = 8;

        // first run has no accounts, so the first signup becomes the admin
        public static int CountUsers()
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT COUNT(*) FROM users";
            using var cmd = new MySqlCommand(query, conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetUser()
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT username,
                                    CONCAT(first_name, ' ', last_name) AS full_name,
                                    email,
                                    birthdate,
                                    role,
                                    CASE
                                        WHEN is_active = 0 THEN 'Inactive'
                                        WHEN is_approved = 0 THEN 'Pending approval'
                                        ELSE 'Active'
                                    END AS status
                             FROM users
                             ORDER BY username";
            MySqlDataAdapter adapter = new(query, conn);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetUserProfile(string username)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT email, first_name, last_name, birthdate
                             FROM users
                             WHERE username = @username";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        // how many admins could still sign in
        public static int CountActiveAdmins()
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT COUNT(*) FROM users WHERE role = 'ADMIN' AND is_active = 1";
            using var cmd = new MySqlCommand(query, conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool AddUser(string username, string password, string email,
                                   string firstName, string lastName,
                                   DateTime? birthdate, string role, bool isApproved,
                                   string createdBy)
        {
            username = username.Trim();
            email = email.Trim();
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            string hashPassword = PasswordHelper.HashPassword(password);

            using var conn = DatabaseConnection.GetConnection();
            string query = @"INSERT INTO users(username, password, email, first_name, last_name, birthdate, role, is_active, is_approved)
                             VALUES
                             (@username, @hashPassword, @email, @firstName, @lastName, @birthdate, @role, TRUE, @isApproved)";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@hashPassword", hashPassword);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@birthdate", (object?)birthdate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@isApproved", isApproved);

            try
            {
                bool added = cmd.ExecuteNonQuery() > 0;

                if (added)
                {
                    if (createdBy == "")
                    {
                        ActivityLogService.Add("users", $"Created account '{username}'.");
                    }
                    else
                    {
                        ActivityLogService.Add("users", $"Admin '{createdBy}' created account '{username}'.");
                    }
                }

                return added;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; } // username or email already taken
                throw;
            }
        }

        public static bool SetUserActive(string username, bool isActive)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"UPDATE users
                             SET is_active = @isActive
                             WHERE username = @username AND is_active <> @isActive";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@isActive", isActive);
            cmd.Parameters.AddWithValue("@username", username);
            bool changed = cmd.ExecuteNonQuery() > 0;

            if (changed)
            {
                string action = isActive ? "Reactivated" : "Deactivated";
                ActivityLogService.Add("users", $"{action} account '{username}'.");
            }

            return changed;
        }

        public static bool SetUserApproved(string username, bool isApproved, string approvedBy)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"UPDATE users
                             SET is_approved = @isApproved
                             WHERE username = @username AND is_approved <> @isApproved";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@isApproved", isApproved);
            cmd.Parameters.AddWithValue("@username", username);
            bool changed = cmd.ExecuteNonQuery() > 0;

            if (changed && isApproved)
            {
                ActivityLogService.Add("users", $"Admin '{approvedBy}' approved account '{username}'.");
            }

            return changed;
        }

        public static bool UpdateUser(string oldUsername, string newUsername, string role, string newPassword,
                                      string email, string firstName, string lastName, DateTime? birthdate)
        {
            newUsername = newUsername.Trim();
            email = email.Trim();
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            bool changePassword = newPassword != "";

            using var conn = DatabaseConnection.GetConnection();
            string query;

            if (changePassword)
            {
                query = @"UPDATE users
                          SET username = @newUsername, email = @email,
                              first_name = @firstName, last_name = @lastName,
                              birthdate = @birthdate, role = @role, password = @password
                          WHERE username = @oldUsername";
            }
            else
            {
                query = @"UPDATE users
                          SET username = @newUsername, email = @email,
                              first_name = @firstName, last_name = @lastName,
                              birthdate = @birthdate, role = @role
                          WHERE username = @oldUsername";
            }

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@newUsername", newUsername);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@birthdate", (object?)birthdate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@oldUsername", oldUsername);

            if (changePassword)
            {
                cmd.Parameters.AddWithValue("@password", PasswordHelper.HashPassword(newPassword));
            }

            try
            {
                bool updated = cmd.ExecuteNonQuery() > 0;

                if (updated)
                {
                    ActivityLogService.Add("users", $"Edited account '{newUsername}'.");
                }

                return updated;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) { return false; } // new username or email already taken
                throw;
            }
        }
    }
}
