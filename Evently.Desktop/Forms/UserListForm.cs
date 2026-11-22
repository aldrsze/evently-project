using Evently.Core.Services;
using Evently.Helpers;

namespace Evently.Forms
{
    public partial class UserListForm : Form
    {
        public UserListForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            usersDataGridView.DataSource = null;
            usersDataGridView.DataSource = UserService.GetUser();
        }

        private void AddButton_Click(object sender, EventArgs e) 
        {
            string username = usernameTextBox.Text.Trim();
            string firstName = firstNameTextBox.Text.Trim();
            string lastName = lastNameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string role = roleComboBox.Text;

            if (username == "" || firstName == "" || lastName == "" || email == "" || password == "" || role == "")
            {
                MessageBox.Show("Fill in username, name, email, password, and role.", "Add account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < UserService.MinPasswordLength)
            {
                MessageBox.Show("Use at least " + UserService.MinPasswordLength + " characters for the password.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // an admin adding an account approves it right away
                if (UserService.AddUser(username, password, email, firstName, lastName, null, role, true))
                {
                    MessageBox.Show("Account created. It can sign in right away.", "Account added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    usernameTextBox.Text = "";
                    firstNameTextBox.Text = "";
                    lastNameTextBox.Text = "";
                    emailTextBox.Text = "";
                    passwordTextBox.Text = "";
                    LoadUsers();
                }
                else { MessageBox.Show("That username or email is already taken. Choose another one.", "Account taken", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeactivateButton_Click(object sender, EventArgs e)
        {
            SetSelectedUserActive(false);
        }

        private void ReactivateButton_Click(object sender, EventArgs e)
        {
            SetSelectedUserActive(true);
        }

        private void SetSelectedUserActive(bool isActive)
        {
            if (usersDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select a user in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = Convert.ToString(usersDataGridView.CurrentRow.Cells["usernameColumn"].Value) ?? "";
            username = username.Trim();

            bool isSelf = string.Equals(username, Session.CurrentUsername, StringComparison.OrdinalIgnoreCase);

            if (isActive == false && isSelf)
            {
                MessageBox.Show("You cannot deactivate the account you are signed in with.", "Own account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isActive == false && WouldRemoveLastAdmin(username))
            {
                MessageBox.Show("This is the only active admin. Create another admin before deactivating this one.", "Last active admin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (UserService.SetUserActive(username, isActive))
                {
                    if (isActive) { MessageBox.Show("Account reactivated. It can sign in again.", "Account updated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { MessageBox.Show("Account deactivated. It can no longer sign in.", "Account updated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Nothing changed. The account is missing or already in that state.", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select a user in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = Convert.ToString(usersDataGridView.CurrentRow.Cells["usernameColumn"].Value) ?? "";
            string email = Convert.ToString(usersDataGridView.CurrentRow.Cells["emailColumn"].Value) ?? "";
            string fullName = Convert.ToString(usersDataGridView.CurrentRow.Cells["fullNameColumn"].Value) ?? username;
            username = username.Trim();
            email = email.Trim();

            try
            {
                if (UserService.SetUserApproved(username, true))
                {
                    bool emailSent = false;
                    if (!string.IsNullOrEmpty(email))
                    {
                        emailSent = EmailService.SendAccountApprovedEmail(email, fullName);
                    }

                    if (emailSent)
                    {
                        MessageBox.Show("Account approved and notification email sent.", "Account approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Account approved. (Notification email could not be sent. Check SMTP settings).", "Account approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Nothing changed. The account is missing or already approved.", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool WouldRemoveLastAdmin(string username)
        {
            string role = AuthService.GetRole(username) ?? "";

            if (role != "ADMIN")
            {
                return false; // not an admin, nothing to protect
            }

            return UserService.CountActiveAdmins() <= 1;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Select a user in the list first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = Convert.ToString(usersDataGridView.CurrentRow.Cells["usernameColumn"].Value) ?? "";
            string role = Convert.ToString(usersDataGridView.CurrentRow.Cells["roleColumn"].Value) ?? "";

            // nobody changes their own role
            bool lockRole = string.Equals(username, Session.CurrentUsername, StringComparison.OrdinalIgnoreCase);

            using UserEditForm dialog = new(username, role, lockRole);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveUserChanges(username, dialog);
            }
        }

        private void SaveUserChanges(string oldUsername, UserEditForm dialog)
        {
            string newUsername = dialog.UsernameValue.Trim();
            string email = dialog.EmailValue.Trim();
            string firstName = dialog.FirstNameValue.Trim();
            string lastName = dialog.LastNameValue.Trim();
            string role = dialog.RoleValue;
            string newPassword = dialog.NewPasswordValue;

            if (newUsername == "" || email == "" || firstName == "" || lastName == "")
            {
                MessageBox.Show("Enter a username, name, and email.", "Edit account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != "" && newPassword.Length < UserService.MinPasswordLength)
            {
                MessageBox.Show("Use at least " + UserService.MinPasswordLength + " characters for the password.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (role != "ADMIN" && WouldRemoveLastAdmin(oldUsername))
            {
                MessageBox.Show("This is the only active admin. Give another account the admin role first.", "Last active admin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (UserService.UpdateUser(oldUsername, newUsername, role, newPassword,
                                           email, firstName, lastName, dialog.BirthdateValue))
                {
                    if (string.Equals(oldUsername, Session.CurrentUsername, StringComparison.OrdinalIgnoreCase))
                    {
                        Session.RenameCurrentUser(newUsername);
                    }
                    MessageBox.Show("Account updated.", "Account saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("That username or email is already taken. Choose another one.", "Account taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The change could not be saved. " + ex.Message, "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}