using System.Data;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class UserEditForm : Form
    {
        public UserEditForm(string username, string role, bool lockRole)
        {
            InitializeComponent();
            usernameTextBox.Text = username;
            roleComboBox.Text = role;
            LoadProfile(username);

            if (lockRole)
            {
                // nobody changes their own role
                roleComboBox.Enabled = false;
            }
        }

        private void LoadProfile(string username)
        {
            DataTable profile = UserService.GetUserProfile(username);
            if (profile.Rows.Count != 1)
            {
                return;
            }

            DataRow row = profile.Rows[0];
            firstNameTextBox.Text = Convert.ToString(row["first_name"]) ?? "";
            lastNameTextBox.Text = Convert.ToString(row["last_name"]) ?? "";
            emailTextBox.Text = Convert.ToString(row["email"]) ?? "";

            object birthdate = row["birthdate"];
            if (birthdate != DBNull.Value)
            {
                birthdatePicker.Value = Convert.ToDateTime(birthdate);
                birthdatePicker.Checked = true;
            }
        }

        // values the caller reads after the dialog closes with OK
        public string UsernameValue
        {
            get { return usernameTextBox.Text; }
        }

        public string FirstNameValue
        {
            get { return firstNameTextBox.Text; }
        }

        public string LastNameValue
        {
            get { return lastNameTextBox.Text; }
        }

        public string EmailValue
        {
            get { return emailTextBox.Text; }
        }

        public DateTime? BirthdateValue
        {
            get
            {
                if (birthdatePicker.Checked)
                {
                    return birthdatePicker.Value.Date;
                }
                return null;
            }
        }

        public string RoleValue
        {
            get { return roleComboBox.Text; }
        }

        public string NewPasswordValue
        {
            get { return passwordTextBox.Text; }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string firstName = firstNameTextBox.Text.Trim();
            string lastName = lastNameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();

            if (username == "" || firstName == "" || lastName == "" || email == "")
            {
                MessageBox.Show("Enter a username, name, and email.", "Edit account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains('@'))
            {
                MessageBox.Show("Enter a valid email address.", "Edit account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (passwordTextBox.Text != "" && passwordTextBox.Text.Length < UserService.MinPasswordLength)
            {
                MessageBox.Show("Use at least " + UserService.MinPasswordLength + " characters for the password.", "Edit account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            usernameTextBox.Text = username;
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
