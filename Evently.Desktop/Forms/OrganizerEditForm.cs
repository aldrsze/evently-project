using System;
using System.Windows.Forms;

namespace Evently.Forms
{
    public partial class OrganizerEditForm : Form
    {
        private readonly int organizerId;

        public OrganizerEditForm()
        {
            InitializeComponent();
        }

        public OrganizerEditForm(int organizerId, string fullName, string email, string phone, string organization)
        {
            InitializeComponent();
            this.organizerId = organizerId;
            nameTextBox.Text = fullName;
            emailTextBox.Text = email;
            phoneTextBox.Text = phone;
            organizationTextBox.Text = organization;
        }

        public int OrganizerIdValue
        {
            get { return organizerId; }
        }

        public string FullNameValue
        {
            get { return nameTextBox.Text.Trim(); }
        }

        public string EmailValue
        {
            get { return emailTextBox.Text.Trim(); }
        }

        public string PhoneValue
        {
            get { return phoneTextBox.Text.Trim(); }
        }

        public string OrganizationValue
        {
            get { return organizationTextBox.Text.Trim(); }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.Focus();
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
