using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Evently.Core.Helpers;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class AttendeesForm : Form
    {
        public AttendeesForm()
        {
            InitializeComponent();
            attendeesDataGridView.AutoGenerateColumns = false;
            LoadEvents();
        }

        private void LoadEvents()
        {
            DataTable eventsTable = EventService.GetEvent();
            eventComboBox.DisplayMember = "name";
            eventComboBox.ValueMember = "event_id";
            eventComboBox.DataSource = eventsTable;

            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadAttendees(eventId);
            }
        }

        private void LoadAttendees(int eventId)
        {
            attendeesDataGridView.DataSource = TicketService.GetAttendeesByEvent(eventId);
            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            string latest = ActivityLogService.GetLatest("attendees");

            if (latest == "")
            {
                latest = "No activity yet.";
            }

            activityLogLabel.Text = latest;
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadAttendees(eventId);
            }
        }

        private void AddAttendeeButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Select an event first.", "No Event", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!OrganizerService.EventHasManager(eventId))
            {
                MessageBox.Show("This event has no manager. Assign a manager before registering attendees.", "No Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = firstNameTextBox.Text.Trim();
            string lastName = lastNameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            DateTime birthdate = birthdateDateTimePicker.Value;

            if (firstName == "" || lastName == "" || email == "")
            {
                MessageBox.Show("Fill in first name, last name, and email.", "Add Attendee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Enter a valid email address.", "Add Attendee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (TicketService.AddAttendee(eventId, firstName, lastName, email, birthdate))
                {
                    MessageBox.Show("Attendee added to event.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    firstNameTextBox.Clear();
                    lastNameTextBox.Clear();
                    emailTextBox.Clear();
                    birthdateDateTimePicker.Value = DateTime.Today;
                    LoadAttendees(eventId);
                }
                else
                {
                    MessageBox.Show("Could not add attendee. The attendee may already be registered for this event.", "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving attendee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (attendeesDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an attendee in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                return;
            }

            int attendeeId = Convert.ToInt32(attendeesDataGridView.CurrentRow.Cells["attendeeIdColumn"].Value);
            string firstName = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["firstNameColumn"].Value) ?? "";
            string lastName = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["lastNameColumn"].Value) ?? "";
            string email = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["emailColumn"].Value) ?? "";
            DateTime birthdate = DateTime.Today;
            object? bVal = attendeesDataGridView.CurrentRow.Cells["birthdateColumn"].Value;
            if (bVal != null && bVal != DBNull.Value && DateTime.TryParse(bVal.ToString(), out DateTime parsed))
            {
                birthdate = parsed;
            }

            using AttendeeEditForm dialog = new(attendeeId, firstName, lastName, email, birthdate);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (TicketService.UpdateAttendee(eventId, dialog.AttendeeIdValue, dialog.FirstNameValue, dialog.LastNameValue, dialog.EmailValue, dialog.BirthdateValue))
                    {
                        MessageBox.Show("Attendee updated.", "Attendee saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAttendees(eventId);
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Email may already be in use.", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (attendeesDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an attendee in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                return;
            }

            int attendeeId = Convert.ToInt32(attendeesDataGridView.CurrentRow.Cells["attendeeIdColumn"].Value);
            string attendeeName = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["attendeeNameColumn"].Value) ?? "this attendee";

            DialogResult confirm = MessageBox.Show(
                $"Remove '{attendeeName}' from this event?\n\nWarning: This will also delete their ticket for this event.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (TicketService.DeleteAttendee(eventId, attendeeId))
                    {
                        MessageBox.Show("Attendee removed.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAttendees(eventId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not remove attendee: " + ex.Message, "Delete failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ImportCsvButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!OrganizerService.EventHasManager(eventId))
            {
                MessageBox.Show("This event has no manager. Assign a manager before importing attendees.", "No Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog ofd = new();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            ofd.Title = "Select Attendees CSV";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    resultLabel.Text = "Importing attendees...";
                    Application.DoEvents();

                    CsvImportResult result = CsvService.ImportAttendeesFromCsv(eventId, ofd.FileName);

                    string summary = $"{result.Imported} imported, {result.Duplicates} duplicates skipped, {result.Invalid} invalid, {result.OverCapacity} over capacity.";
                    resultLabel.Text = summary;
                    MessageBox.Show(summary, "Import Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAttendees(eventId);
                }
                catch (Exception ex)
                {
                    resultLabel.Text = "Import failed.";
                    MessageBox.Show("Could not import CSV: " + ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void SendTicketButton_Click(object sender, EventArgs e)
        {
            if (attendeesDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an attendee in the list first.", "Send Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ticketId = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["ticketIdColumn"].Value) ?? "";
            if (ticketId.Length == 0)
            {
                MessageBox.Show("Could not find ticket ID for selected attendee.", "Send Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string isEmailed = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["ticketEmailedColumn"].Value) ?? "No";
            if (isEmailed == "Yes")
            {
                DialogResult resend = MessageBox.Show(
                    "Ticket has already been sent to this attendee. Resend anyway?",
                    "Resend Ticket",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resend != DialogResult.Yes)
                {
                    return;
                }
            }

            DataRow? ticketRow = TicketService.GetTicketDetails(ticketId);
            if (ticketRow == null)
            {
                MessageBox.Show("Ticket details not found.", "Send Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string recipientEmail = ticketRow["email"].ToString() ?? "";
            string attendeeName = ticketRow["attendee_name"].ToString() ?? "";
            string eventName = ticketRow["event_name"].ToString() ?? "";
            string venue = ticketRow["venue"].ToString() ?? "";
            DateTime eventDate = Convert.ToDateTime(ticketRow["event_date"]);

            sendTicketsButton.Enabled = false;
            sendToAllButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            resultLabel.Text = $"Sending ticket to {attendeeName}...";
            Application.DoEvents();

            bool success = false;
            try
            {
                success = EmailService.SendTicketEmail(recipientEmail, attendeeName, eventName, venue, eventDate, ticketId);
            }
            finally
            {
                Cursor = Cursors.Default;
                sendTicketsButton.Enabled = true;
                sendToAllButton.Enabled = true;
                resultLabel.Text = "";
            }

            if (success)
            {
                TicketService.MarkTicketAsEmailed(ticketId);
                ActivityLogService.Add("tickets", $"Emailed ticket to {attendeeName} ({recipientEmail}) for '{eventName}'.");
                MessageBox.Show($"Ticket sent to {attendeeName} ({recipientEmail}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
                {
                    LoadAttendees(eventId);
                }
            }
            else
            {
                MessageBox.Show("Failed to send ticket. Please check your SMTP settings in db.config.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendToAllButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Send Tickets", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable unemailedTickets = TicketService.GetUnemailedTicketsByEvent(eventId);
            if (unemailedTickets.Rows.Count == 0)
            {
                MessageBox.Show("All attendees for this event have already received their tickets.", "Send Tickets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int sentCount = 0;
            int failedCount = 0;
            int total = unemailedTickets.Rows.Count;

            sendTicketsButton.Enabled = false;
            sendToAllButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                for (int i = 0; i < total; i++)
                {
                    DataRow row = unemailedTickets.Rows[i];
                    string ticketId = row["ticket_id"].ToString() ?? "";
                    string recipientEmail = row["email"].ToString() ?? "";
                    string attendeeName = row["attendee_name"].ToString() ?? "";
                    string eventName = row["event_name"].ToString() ?? "";
                    string venue = row["venue"].ToString() ?? "";
                    DateTime eventDate = Convert.ToDateTime(row["event_date"]);

                    resultLabel.Text = $"Sending ticket {i + 1} of {total} to {attendeeName}...";
                    Application.DoEvents();

                    bool success = EmailService.SendTicketEmail(recipientEmail, attendeeName, eventName, venue, eventDate, ticketId);
                    if (success)
                    {
                        TicketService.MarkTicketAsEmailed(ticketId);
                        ActivityLogService.Add("tickets", $"Emailed ticket to {attendeeName} ({recipientEmail}) for '{eventName}'.");
                        sentCount++;
                    }
                    else
                    {
                        failedCount++;
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                sendTicketsButton.Enabled = true;
                sendToAllButton.Enabled = true;
                resultLabel.Text = "";
            }

            string resultMsg = $"Tickets sent: {sentCount}";
            if (failedCount > 0)
            {
                resultMsg += $"\nFailed: {failedCount}";
            }

            resultLabel.Text = resultMsg;
            MessageBox.Show(resultMsg, "Send Tickets", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAttendees(eventId);
        }

        private static string GetWebBaseUrl()
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "db.config");
            if (File.Exists(configPath))
            {
                string[] lines = File.ReadAllLines(configPath);
                string val = DatabaseConnection.ReadValue(lines, "WebBaseUrl");
                if (!string.IsNullOrEmpty(val))
                {
                    return val.TrimEnd('/');
                }
            }
            return "http://localhost:5000";
        }

        private void SendReviewButton_Click(object sender, EventArgs e)
        {
            if (attendeesDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an attendee in the list first.", "Send Review", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ticketId = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["ticketIdColumn"].Value) ?? "";
            if (ticketId.Length == 0)
            {
                MessageBox.Show("Could not find ticket ID for selected attendee.", "Send Review", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow? ticketRow = ReviewService.GetTicketForReview(ticketId);
            if (ticketRow == null)
            {
                MessageBox.Show("Ticket details not found.", "Send Review", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ticketRow["checked_in_at"] == DBNull.Value)
            {
                MessageBox.Show("This attendee has not checked in yet. Reviews can only be sent to checked-in attendees.", "Not Checked In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToInt32(ticketRow["has_review"]) > 0)
            {
                MessageBox.Show("This attendee has already submitted a review.", "Review Already Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string isReviewEmailed = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["reviewEmailedColumn"].Value) ?? "No";
            if (isReviewEmailed == "Yes")
            {
                DialogResult resend = MessageBox.Show(
                    "Review request has already been sent to this attendee. Resend anyway?",
                    "Resend Review",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resend != DialogResult.Yes)
                {
                    return;
                }
            }

            string recipientEmail = Convert.ToString(attendeesDataGridView.CurrentRow.Cells["emailColumn"].Value) ?? "";
            string attendeeName = ticketRow["attendee_name"].ToString() ?? "";
            string eventName = ticketRow["event_name"].ToString() ?? "";
            string reviewUrl = $"{GetWebBaseUrl()}/Review/{ticketId}";

            sendReviewButton.Enabled = false;
            sendReviewsAllButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            resultLabel.Text = $"Sending review request to {attendeeName}...";
            Application.DoEvents();

            bool success = false;
            try
            {
                success = EmailService.SendReviewRequestEmail(recipientEmail, attendeeName, eventName, reviewUrl);
            }
            finally
            {
                Cursor = Cursors.Default;
                sendReviewButton.Enabled = true;
                sendReviewsAllButton.Enabled = true;
                resultLabel.Text = "";
            }

            if (success)
            {
                ReviewService.MarkReviewAsEmailed(ticketId);
                MessageBox.Show($"Review request sent to {attendeeName} ({recipientEmail}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
                {
                    LoadAttendees(eventId);
                }
            }
            else
            {
                MessageBox.Show("Failed to send review request. Please check your SMTP settings in db.config.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendReviewsAllButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Send Reviews", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable unemailedTickets = ReviewService.GetUnemailedCheckedInTickets(eventId);
            if (unemailedTickets.Rows.Count == 0)
            {
                MessageBox.Show("No checked-in attendees are waiting for review requests.", "Send Reviews", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int sentCount = 0;
            int failedCount = 0;
            int total = unemailedTickets.Rows.Count;
            string baseUrl = GetWebBaseUrl();

            sendReviewButton.Enabled = false;
            sendReviewsAllButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                for (int i = 0; i < total; i++)
                {
                    DataRow row = unemailedTickets.Rows[i];
                    string ticketId = row["ticket_id"].ToString() ?? "";
                    string recipientEmail = row["email"].ToString() ?? "";
                    string attendeeName = row["attendee_name"].ToString() ?? "";
                    string eventName = row["event_name"].ToString() ?? "";
                    string reviewUrl = $"{baseUrl}/Review/{ticketId}";

                    resultLabel.Text = $"Sending review request {i + 1} of {total} to {attendeeName}...";
                    Application.DoEvents();

                    bool success = EmailService.SendReviewRequestEmail(recipientEmail, attendeeName, eventName, reviewUrl);
                    if (success)
                    {
                        ReviewService.MarkReviewAsEmailed(ticketId);
                        sentCount++;
                    }
                    else
                    {
                        failedCount++;
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                sendReviewButton.Enabled = true;
                sendReviewsAllButton.Enabled = true;
                resultLabel.Text = "";
            }

            string resultMsg = $"Review requests sent: {sentCount}";
            if (failedCount > 0)
            {
                resultMsg += $"\nFailed: {failedCount}";
            }

            resultLabel.Text = resultMsg;
            MessageBox.Show(resultMsg, "Send Reviews", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAttendees(eventId);
        }

        private void ExportCsvButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string eventName = eventComboBox.Text.Trim();
            string safeEventName = string.Join("_", eventName.Split(Path.GetInvalidFileNameChars()));

            using SaveFileDialog sfd = new();
            sfd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            sfd.FileName = $"{safeEventName}_attendance_{DateTime.Now:yyyyMMdd}.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    resultLabel.Text = "Exporting CSV...";
                    Application.DoEvents();

                    CsvService.ExportAttendanceToCsv(eventId, sfd.FileName);

                    resultLabel.Text = "Export completed.";
                    MessageBox.Show("Attendance exported successfully.", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    resultLabel.Text = "Export failed.";
                    MessageBox.Show("Could not export CSV: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
    }
}
