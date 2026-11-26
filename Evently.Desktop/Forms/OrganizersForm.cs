using System;
using System.Data;
using System.Windows.Forms;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class OrganizersForm : Form
    {
        private DataTable availableManagers = new();

        public OrganizersForm()
        {
            InitializeComponent();
            Load += OrganizersForm_Load;
            organizersDataGridView.CellDoubleClick += OrganizersDataGridView_CellDoubleClick;
        }

        private void OrganizersDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedManager();
            }
        }

        private void EditManagerButton_Click(object? sender, EventArgs e)
        {
            EditSelectedManager();
        }

        private void EditSelectedManager()
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Edit Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (organizersDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select a manager to edit from the list.", "Edit Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int organizerId = Convert.ToInt32(organizersDataGridView.CurrentRow.Cells["organizerIdColumn"].Value);
            string name = Convert.ToString(organizersDataGridView.CurrentRow.Cells["nameColumn"].Value) ?? "";
            string email = Convert.ToString(organizersDataGridView.CurrentRow.Cells["emailColumn"].Value) ?? "";
            string phone = Convert.ToString(organizersDataGridView.CurrentRow.Cells["phoneColumn"].Value) ?? "";
            string organization = Convert.ToString(organizersDataGridView.CurrentRow.Cells["organizationColumn"].Value) ?? "";

            using OrganizerEditForm editForm = new(organizerId, name, email, phone, organization);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bool updated = OrganizerService.UpdateOrganizer(
                        editForm.OrganizerIdValue,
                        editForm.FullNameValue,
                        editForm.EmailValue,
                        editForm.PhoneValue,
                        editForm.OrganizationValue);

                    if (updated)
                    {
                        resultLabel.Text = $"Updated manager '{editForm.FullNameValue}'.";
                        LoadManagers(eventId);
                    }
                    else
                    {
                        MessageBox.Show("Could not update manager. The email may already be in use.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update manager: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OrganizersForm_Load(object? sender, EventArgs e)
        {
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                DataTable events = EventService.GetEvent();
                eventComboBox.DataSource = events;
                eventComboBox.DisplayMember = "name";
                eventComboBox.ValueMember = "event_id";

                if (events.Rows.Count > 0)
                {
                    eventComboBox.SelectedIndex = 0;
                    if (int.TryParse(eventComboBox.SelectedValue?.ToString(), out int eventId))
                    {
                        LoadManagers(eventId);
                    }
                }
                else
                {
                    organizersDataGridView.DataSource = null;
                    resultLabel.Text = "No events yet.";
                }
            }
            catch (Exception ex)
            {
                resultLabel.Text = "Error loading events.";
                MessageBox.Show("Could not load events: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadManagers(int eventId)
        {
            try
            {
                DataTable managers = OrganizerService.GetOrganizersByEvent(eventId);
                organizersDataGridView.AutoGenerateColumns = false;
                organizersDataGridView.DataSource = managers;

                if (managers.Rows.Count > 0)
                {
                    resultLabel.Text = $"Showing {managers.Rows.Count} manager(s).";
                }
                else
                {
                    resultLabel.Text = "This event has no manager. Registration is blocked until one is added.";
                }

                LoadAvailableManagers(eventId);
                RefreshActivityLog();
            }
            catch (Exception ex)
            {
                resultLabel.Text = "Error loading managers.";
                MessageBox.Show("Could not load managers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshActivityLog()
        {
            string latest = ActivityLogService.GetLatest("organizers");

            if (latest == "")
            {
                latest = "No activity yet.";
            }

            activityLogLabel.Text = latest;
        }

        private void LoadAvailableManagers(int eventId)
        {
            try
            {
                availableManagers = OrganizerService.GetAvailableOrganizers(eventId);
                emailComboBox.Items.Clear();

                for (int i = 0; i < availableManagers.Rows.Count; i++)
                {
                    DataRow row = availableManagers.Rows[i];
                    string email = Convert.ToString(row["email"]) ?? "";
                    emailComboBox.Items.Add(email);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load available managers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmailComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplySelectedEmailInfo();
        }

        private void EmailComboBox_TextChanged(object? sender, EventArgs e)
        {
            ApplySelectedEmailInfo();
        }

        private void ApplySelectedEmailInfo()
        {
            string email = emailComboBox.Text.Trim();
            DataRow? match = null;

            for (int i = 0; i < availableManagers.Rows.Count; i++)
            {
                string rowEmail = Convert.ToString(availableManagers.Rows[i]["email"]) ?? "";
                if (string.Equals(rowEmail, email, StringComparison.OrdinalIgnoreCase))
                {
                    match = availableManagers.Rows[i];
                    break;
                }
            }

            if (match != null)
            {
                nameTextBox.Text = Convert.ToString(match["full_name"]) ?? "";
                phoneTextBox.Text = Convert.ToString(match["phone"]) ?? "";
                organizationTextBox.Text = Convert.ToString(match["organization"]) ?? "";

                nameTextBox.ReadOnly = true;
                phoneTextBox.ReadOnly = true;
                organizationTextBox.ReadOnly = true;
            }
            else
            {
                nameTextBox.ReadOnly = false;
                phoneTextBox.ReadOnly = false;
                organizationTextBox.ReadOnly = false;
            }
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadManagers(eventId);
            }
        }

        private void AddManagerButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Add Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string email = emailComboBox.Text.Trim();
            string name = nameTextBox.Text.Trim();
            string phone = phoneTextBox.Text.Trim();
            string organization = organizationTextBox.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailComboBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (ManagerEmailAlreadyListed(email))
            {
                MessageBox.Show("This email already belongs to a manager on this event.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = OrganizerService.AddOrganizerToEvent(eventId, name, email, phone, organization);
                if (success)
                {
                    nameTextBox.Clear();
                    emailComboBox.Text = "";
                    phoneTextBox.Clear();
                    organizationTextBox.Clear();
                    nameTextBox.ReadOnly = false;
                    phoneTextBox.ReadOnly = false;
                    organizationTextBox.ReadOnly = false;
                    resultLabel.Text = $"Added manager '{name}'.";
                    LoadManagers(eventId);
                }
                else
                {
                    MessageBox.Show("Could not add manager. The email may already be linked to this event.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not add manager: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // one email can only belong to one manager, so an email already in the grid is a duplicate
        private bool ManagerEmailAlreadyListed(string email)
        {
            foreach (DataGridViewRow gridRow in organizersDataGridView.Rows)
            {
                string rowEmail = (Convert.ToString(gridRow.Cells["emailColumn"].Value) ?? "").Trim();

                if (rowEmail.ToLower() == email.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private void RemoveManagerButton_Click(object? sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue == null || !int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                MessageBox.Show("Please select an event first.", "Remove Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (organizersDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select a manager to remove from the list.", "Remove Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int organizerId = Convert.ToInt32(organizersDataGridView.CurrentRow.Cells["organizerIdColumn"].Value);
            string name = Convert.ToString(organizersDataGridView.CurrentRow.Cells["nameColumn"].Value) ?? "this manager";

            DialogResult confirm = MessageBox.Show($"Remove '{name}' from this event?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool removed = OrganizerService.RemoveOrganizerFromEvent(eventId, organizerId);
                if (removed)
                {
                    resultLabel.Text = $"Removed '{name}' from event.";
                    LoadManagers(eventId);
                }
                else
                {
                    MessageBox.Show("Could not remove manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not remove manager: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
