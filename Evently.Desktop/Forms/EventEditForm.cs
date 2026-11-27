namespace Evently.Forms
{
    public partial class EventEditForm : Form
    {
        private readonly int eventId;

        public EventEditForm()
        {
            InitializeComponent();
        }

        // used by Edit: fills the fields with the selected event
        public EventEditForm(int eventId, string name, string venue, string description, DateTime eventDate, int capacity, DateTime? registrationDeadline)
        {
            InitializeComponent();
            this.eventId = eventId;
            nameTextBox.Text = name;
            venueTextBox.Text = venue;
            descriptionTextBox.Text = description;
            eventDatePicker.Value = eventDate;
            capacityNumericUpDown.Value = capacity;

            if (registrationDeadline.HasValue)
            {
                registrationDeadlinePicker.Value = registrationDeadline.Value;
                registrationDeadlinePicker.Checked = true;
            }
            else
            {
                registrationDeadlinePicker.Checked = false;
            }
        }

        // the id the caller passed in, so an update knows which row to change
        public int EventIdValue
        {
            get { return eventId; }
        }

        // edited values, read by the caller after the dialog closes with OK
        public string NameValue
        {
            get { return nameTextBox.Text; }
        }

        public string VenueValue
        {
            get { return venueTextBox.Text; }
        }

        public string DescriptionValue
        {
            get { return descriptionTextBox.Text; }
        }

        public DateTime EventDateValue
        {
            get { return eventDatePicker.Value; }
        }

        public int CapacityValue
        {
            get { return (int)capacityNumericUpDown.Value; }
        }

        // empty box means registration closes at the event start
        public DateTime? RegistrationDeadlineValue
        {
            get
            {
                if (registrationDeadlinePicker.Checked)
                {
                    return registrationDeadlinePicker.Value;
                }

                return null;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Enter an event name.", "Event", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (registrationDeadlinePicker.Checked && registrationDeadlinePicker.Value > eventDatePicker.Value)
            {
                MessageBox.Show("The registration deadline cannot be after the event date.", "Event", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
