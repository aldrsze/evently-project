using System;
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class ReviewService
    {
        public static DataTable GetReviewsByEvent(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                r.rating,
                                r.comment,
                                r.submitted_at
                             FROM reviews r
                             INNER JOIN tickets t ON r.ticket_id = t.ticket_id
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             WHERE t.event_id = @eventId
                             ORDER BY r.submitted_at DESC;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetUnemailedCheckedInTickets(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                t.ticket_id,
                                a.email,
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                e.name AS event_name
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             INNER JOIN events e ON t.event_id = e.event_id
                             LEFT JOIN reviews r ON t.ticket_id = r.ticket_id
                             WHERE t.event_id = @eventId
                               AND t.checked_in_at IS NOT NULL
                               AND t.review_emailed = FALSE
                               AND r.ticket_id IS NULL;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static bool MarkReviewAsEmailed(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "UPDATE tickets SET review_emailed = TRUE WHERE ticket_id = @ticketId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public static DataRow? GetTicketForReview(string ticketId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                t.ticket_id,
                                t.checked_in_at,
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                e.name AS event_name,
                                e.venue,
                                e.event_date,
                                (SELECT COUNT(*) FROM reviews r WHERE r.ticket_id = t.ticket_id) AS has_review
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             INNER JOIN events e ON t.event_id = e.event_id
                             WHERE t.ticket_id = @ticketId;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return table.Rows[0];
        }

        public static bool SubmitReview(string ticketId, int rating, string? comment)
        {
            if (rating < 1 || rating > 5)
            {
                return false;
            }

            comment = comment?.Trim();
            if (comment != null && comment.Length > 500)
            {
                comment = comment.Substring(0, 500);
            }

            using var conn = DatabaseConnection.GetConnection();

            string checkQuery = @"SELECT t.checked_in_at,
                                         (SELECT COUNT(*) FROM reviews r WHERE r.ticket_id = t.ticket_id) AS has_review
                                  FROM tickets t
                                  WHERE t.ticket_id = @ticketId;";

            using (MySqlCommand checkCmd = new(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@ticketId", ticketId);
                using MySqlDataReader reader = checkCmd.ExecuteReader();
                if (!reader.Read())
                {
                    return false;
                }

                if (reader.IsDBNull(reader.GetOrdinal("checked_in_at")))
                {
                    return false;
                }

                if (Convert.ToInt32(reader["has_review"]) > 0)
                {
                    return false;
                }
            }

            string insertQuery = @"INSERT INTO reviews (ticket_id, rating, comment, submitted_at)
                                   VALUES (@ticketId, @rating, @comment, NOW());";

            using (MySqlCommand insertCmd = new(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@ticketId", ticketId);
                insertCmd.Parameters.AddWithValue("@rating", rating);
                insertCmd.Parameters.AddWithValue("@comment", string.IsNullOrEmpty(comment) ? DBNull.Value : comment);

                try
                {
                    return insertCmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) { return false; }
                    throw;
                }
            }
        }
    }
}
