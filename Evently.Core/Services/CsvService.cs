using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Evently.Core.Helpers;
using MySqlConnector;

namespace Evently.Core.Services
{
    public struct CsvImportResult
    {
        public int Imported;
        public int Duplicates;
        public int Invalid;
        public int OverCapacity;
    }

    public static class CsvService
    {
        public static DataTable GetAttendanceData(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT 
                                a.first_name,
                                a.last_name,
                                a.birthdate,
                                a.email,
                                t.checked_in_at,
                                t.checked_out_at,
                                r.rating,
                                r.comment
                             FROM tickets t
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id
                             LEFT JOIN reviews r ON t.ticket_id = r.ticket_id
                             WHERE t.event_id = @eventId
                             ORDER BY a.last_name, a.first_name;";

            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);

            MySqlDataAdapter adapter = new(cmd);
            DataTable table = new();
            adapter.Fill(table);
            return table;
        }

        public static void ExportAttendanceToCsv(int eventId, string filePath)
        {
            DataTable data = GetAttendanceData(eventId);

            using StreamWriter writer = new(filePath, false, Encoding.UTF8);

            writer.WriteLine("first_name,last_name,birthdate,email,check_in_time,check_out_time,rating,comment");

            foreach (DataRow row in data.Rows)
            {
                string firstName = EscapeCsv(row["first_name"].ToString() ?? "");
                string lastName = EscapeCsv(row["last_name"].ToString() ?? "");
                string birthdate = row["birthdate"] != DBNull.Value ? Convert.ToDateTime(row["birthdate"]).ToString("yyyy-MM-dd") : "";
                string email = EscapeCsv(row["email"].ToString() ?? "");
                string checkIn = row["checked_in_at"] != DBNull.Value ? Convert.ToDateTime(row["checked_in_at"]).ToString("yyyy-MM-dd HH:mm:ss") : "";
                string checkOut = row["checked_out_at"] != DBNull.Value ? Convert.ToDateTime(row["checked_out_at"]).ToString("yyyy-MM-dd HH:mm:ss") : "";
                string rating = row["rating"] != DBNull.Value ? row["rating"].ToString() ?? "" : "";
                string comment = EscapeCsv(row["comment"] != DBNull.Value ? row["comment"].ToString() ?? "" : "");

                writer.WriteLine($"{firstName},{lastName},{birthdate},{email},{checkIn},{checkOut},{rating},{comment}");
            }
        }

        public static CsvImportResult ImportAttendeesFromCsv(int eventId, string filePath)
        {
            CsvImportResult result = new();

            int capacity = GetEventCapacity(eventId);
            int currentCount = GetCurrentTicketCount(eventId);

            using StreamReader reader = new(filePath, Encoding.UTF8);
            string? headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                return result;
            }

            string[] headers = ParseCsvLine(headerLine);
            int fnIdx = -1;
            int lnIdx = -1;
            int emIdx = -1;
            int bdIdx = -1;

            for (int i = 0; i < headers.Length; i++)
            {
                string h = headers[i].Trim().ToLower();
                if (h == "first_name") fnIdx = i;
                else if (h == "last_name") lnIdx = i;
                else if (h == "email") emIdx = i;
                else if (h == "birthdate") bdIdx = i;
            }

            if (fnIdx == -1 || lnIdx == -1 || emIdx == -1)
            {
                result.Invalid++;
                return result;
            }

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = ParseCsvLine(line);
                if (parts.Length <= fnIdx || parts.Length <= lnIdx || parts.Length <= emIdx)
                {
                    result.Invalid++;
                    continue;
                }

                string firstName = parts[fnIdx].Trim();
                string lastName = parts[lnIdx].Trim();
                string email = parts[emIdx].Trim();

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
                {
                    result.Invalid++;
                    continue;
                }

                DateTime birthdate = DateTime.MinValue;
                if (bdIdx != -1 && bdIdx < parts.Length)
                {
                    _ = DateTime.TryParse(parts[bdIdx].Trim(), out birthdate);
                }

                if (capacity > 0 && currentCount >= capacity)
                {
                    result.OverCapacity++;
                    continue;
                }

                if (IsAttendeeInEvent(eventId, email))
                {
                    result.Duplicates++;
                    continue;
                }

                bool added = TicketService.AddAttendee(eventId, firstName, lastName, email, birthdate);
                if (added)
                {
                    result.Imported++;
                    currentCount++;
                }
                else
                {
                    result.Duplicates++;
                }
            }

            return result;
        }

        private static int GetEventCapacity(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT capacity FROM events WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            object? res = cmd.ExecuteScalar();
            return res != null ? Convert.ToInt32(res) : 0;
        }

        private static int GetCurrentTicketCount(int eventId)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = "SELECT COUNT(*) FROM tickets WHERE event_id = @eventId;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            object? res = cmd.ExecuteScalar();
            return res != null ? Convert.ToInt32(res) : 0;
        }

        private static bool IsAttendeeInEvent(int eventId, string email)
        {
            using var conn = DatabaseConnection.GetConnection();
            string query = @"SELECT COUNT(*) 
                             FROM tickets t 
                             INNER JOIN attendees a ON t.attendee_id = a.attendee_id 
                             WHERE t.event_id = @eventId AND a.email = @email;";
            using MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            cmd.Parameters.AddWithValue("@email", email);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static string[] ParseCsvLine(string line)
        {
            List<string> values = new();
            StringBuilder current = new();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        current.Append('\"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    values.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            values.Add(current.ToString());
            return [.. values];
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(',') || value.Contains('\"') || value.Contains('\n') || value.Contains('\r'))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }
            return value;
        }
    }
}
