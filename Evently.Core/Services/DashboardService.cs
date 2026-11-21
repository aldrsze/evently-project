using System;
using System.Data;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public static class DashboardService
    {
        public static DataRow? GetDashboardStats(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                e.capacity,
                                COUNT(t.ticket_id) AS registered,
                                COALESCE(SUM(CASE WHEN t.checked_in_at IS NOT NULL AND t.checked_out_at IS NULL THEN 1 ELSE 0 END), 0) AS currently_inside,
                                COALESCE(SUM(CASE WHEN t.checked_out_at IS NOT NULL THEN 1 ELSE 0 END), 0) AS checked_out,
                                COALESCE(SUM(CASE WHEN t.checked_in_at IS NOT NULL THEN 1 ELSE 0 END), 0) AS total_checked_in,
                                COALESCE(SUM(CASE WHEN t.ticket_emailed = 1 THEN 1 ELSE 0 END), 0) AS tickets_emailed,
                                COALESCE(SUM(CASE WHEN t.review_emailed = 1 THEN 1 ELSE 0 END), 0) AS review_requests_sent,
                                COUNT(r.rating) AS review_count,
                                COALESCE(AVG(r.rating), 0.0) AS average_rating
                             FROM events e
                             LEFT JOIN tickets t ON e.event_id = t.event_id
                             LEFT JOIN reviews r ON t.ticket_id = r.ticket_id
                             WHERE e.event_id = @eventId
                             GROUP BY e.event_id, e.capacity;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return table.Rows[0];
        }

        public static DataTable GetRecentActivity(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                CONCAT(a.first_name, ' ', a.last_name) AS attendee_name,
                                t.checked_in_at,
                                t.checked_out_at
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             WHERE t.event_id = @eventId 
                               AND (t.checked_in_at IS NOT NULL OR t.checked_out_at IS NOT NULL)
                             ORDER BY GREATEST(COALESCE(t.checked_in_at, '1970-01-01'), COALESCE(t.checked_out_at, '1970-01-01')) DESC
                             LIMIT 15;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetCheckInTimeline(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                checked_in_at
                             FROM tickets
                             WHERE event_id = @eventId 
                               AND checked_in_at IS NOT NULL
                             ORDER BY checked_in_at ASC;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }
    }
}
