using System.Data;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class EventListForm : Form
    {
        private DataTable availableManagers = new();

        public EventListForm()
        {
            InitializeComponent();
            LoadEvents();
            LoadOrganizersDropdown();
        }

        private void LoadOrganizersDropdown()
        {
            try
            {
                availableManagers = OrganizerService.GetAllOrganizers();
                managerEmailComboBox.Items.Clear();

                for (int i = 0; i < availableManagers.Rows.Count; i++)
                {
                    DataRow row = availableManagers.Rows[i];
                    string mEmail = Convert.ToString(row["email"]) ?? "";
                    managerEmailComboBox.Items.Add(mEmail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load managers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManagerEmailComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyManagerEmailInfo();
        }

        private void ManagerEmailComboBox_TextChanged(object? sender, EventArgs e)
        {
            ApplyManagerEmailInfo();
        }

        private void ApplyManagerEmailInfo()
        {
            string email = managerEmailComboBox.Text.Trim();
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
                managerNameTextBox.Text = Convert.ToString(match["full_name"]) ?? "";
                managerPhoneTextBox.Text = Convert.ToString(match["phone"]) ?? "";
                managerOrganizationTextBox.Text = Convert.ToString(match["organization"]) ?? "";

                managerNameTextBox.ReadOnly = true;
                managerPhoneTextBox.ReadOnly = true;
                managerOrganizationTextBox.ReadOnly = true;
            }
            else
            {
                managerNameTextBox.ReadOnly = false;
                managerPhoneTextBox.ReadOnly = false;
                managerOrganizationTextBox.ReadOnly = false;
            }
        }

        private void LoadEvents()
        {
            eventsDataGridView.DataSource = null;
            eventsDataGridView.DataSource = EventService.GetEvent();

            if (eventsDataGridView.CurrentRow != null && eventsDataGridView.CurrentRow.Cells["descriptionColumn"] != null)
            {
                selectedDescriptionTextBox.Text = Convert.ToString(eventsDataGridView.CurrentRow.Cells["descriptionColumn"].Value) ?? "";
            }
            else
            {
                selectedDescriptionTextBox.Text = "";
            }

            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            string latest = ActivityLogService.GetLatest("events");

            if (latest == "")
            {
                latest = "No activity yet.";
            }

            activityLogLabel.Text = latest;
        }

        private void EventsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (eventsDataGridView.CurrentRow != null && eventsDataGridView.CurrentRow.Cells["descriptionColumn"] != null)
            {
                selectedDescriptionTextBox.Text = Convert.ToString(eventsDataGridView.CurrentRow.Cells["descriptionColumn"].Value) ?? "";
            }
            else
            {
                selectedDescriptionTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string venue = venueTextBox.Text.Trim();
            DateTime date = eventDatePicker.Value;
            int capacity =  (int) capacityNumericUpDown.Value;
            string desc = descriptionTextBox.Text;

            string managerName = managerNameTextBox.Text.Trim();
            string managerEmail = managerEmailComboBox.Text.Trim();
            string managerPhone = managerPhoneTextBox.Text.Trim();
            string managerOrganization = managerOrganizationTextBox.Text.Trim();

            if (name == "" || venue == "" || desc == "")
            { 
                MessageBox.Show("Fill in name, venue, and description.", "Add event", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            // a manager is optional, but a half-filled manager is not allowed
            bool wantsManager = managerName != "" || managerEmail != "";
            if (wantsManager && (managerName == "" || managerEmail == ""))
            {
                MessageBox.Show("Enter both a manager name and a manager email, or leave both blank.", "Add event", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int newEventId = EventService.AddEvent(name, venue, desc, date, capacity);
                if (newEventId == 0)
                {
                    MessageBox.Show("The event could not be saved. Check the capacity and the field lengths.", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (wantsManager)
                {
                    bool managerAdded = OrganizerService.AddOrganizerToEvent(newEventId, managerName, managerEmail, managerPhone, managerOrganization);
                    if (!managerAdded)
                    {
                        MessageBox.Show("Event created, but the manager could not be linked. Assign a manager before attendees can register.", "Manager not added", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                MessageBox.Show("Event created.", "Event added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameTextBox.Clear();
                venueTextBox.Clear();
                eventDatePicker.Value = DateTime.Now;
                capacityNumericUpDown.Value = 100;
                descriptionTextBox.Clear();
                managerNameTextBox.Clear();
                managerEmailComboBox.Text = "";
                managerPhoneTextBox.Clear();
                managerOrganizationTextBox.Clear();
                managerNameTextBox.ReadOnly = false;
                managerPhoneTextBox.ReadOnly = false;
                managerOrganizationTextBox.ReadOnly = false;
                LoadEvents();
                LoadOrganizersDropdown();
            }
            catch(Exception ex)
            {
                MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditButton_Click(object sender, EventArgs e) 
        {
            if (eventsDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an event in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int eventId = Convert.ToInt32(eventsDataGridView.CurrentRow.Cells[0].Value);
            string name = Convert.ToString(eventsDataGridView.CurrentRow.Cells[1].Value) ?? "";
            string venue = Convert.ToString(eventsDataGridView.CurrentRow.Cells[2].Value) ?? "";
            DateTime eventDate = Convert.ToDateTime(eventsDataGridView.CurrentRow.Cells[3].Value);
            int capacity = Convert.ToInt32(eventsDataGridView.CurrentRow.Cells[4].Value);
            string description = Convert.ToString(eventsDataGridView.CurrentRow.Cells[5].Value) ?? "";

            using EventEditForm dialog = new(eventId, name, venue, description, eventDate, capacity);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (EventService.UpdateEvent(dialog.EventIdValue, dialog.NameValue, dialog.VenueValue, dialog.DescriptionValue, dialog.EventDateValue, dialog.CapacityValue))
                    {
                        MessageBox.Show("Event updated.", "Event saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEvents();
                    }
                    else
                    {
                        MessageBox.Show("Nothing changed. The event may have been deleted.", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // managers are edited in the Event Managers panel, not here
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (eventsDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select an event in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int eventId = Convert.ToInt32(eventsDataGridView.CurrentRow.Cells[0].Value);
            string eventName = Convert.ToString(eventsDataGridView.CurrentRow.Cells[1].Value) ?? "";

            DialogResult answer = MessageBox.Show("Are you sure you want delete " + eventName + "?", "Delete event", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) { return; }

            try
            {
                if (EventService.DeleteEvent(eventId))
                {
                    MessageBox.Show("Event deleted.", "Event deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEvents();
                }
                else
                {
                    MessageBox.Show("The event was already deleted.", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The event could not be deleted. " + ex.Message, "Delete failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}