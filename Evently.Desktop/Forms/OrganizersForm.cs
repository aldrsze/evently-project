using System;
using System.Data;
using System.Windows.Forms;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class OrganizersForm : Form
    {
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
                resultLabel.Text = $"Showing {managers.Rows.Count} manager(s).";
            }
            catch (Exception ex)
            {
                resultLabel.Text = "Error loading managers.";
                MessageBox.Show("Could not load managers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            string name = nameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string phone = phoneTextBox.Text.Trim();
            string organization = organizationTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.Focus();
                return;
            }

            try
            {
                bool success = OrganizerService.AddOrganizerToEvent(eventId, name, email, phone, organization);
                if (success)
                {
                    nameTextBox.Clear();
                    emailTextBox.Clear();
                    phoneTextBox.Clear();
                    organizationTextBox.Clear();
                    resultLabel.Text = $"Added manager '{name}'.";
                    LoadManagers(eventId);
                }
                else
                {
                    MessageBox.Show("Manager is already assigned to this event, or email belongs to another organizer with different details.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not add manager: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
