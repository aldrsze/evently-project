using Evently.Core.Services;
using Evently.Helpers;
using Evently.Core.Helpers;

namespace Evently.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            if (DatabaseConnection.TestConnection())
            {
                MessageBox.Show("Connected to the Evently database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NOT Connected to the Evently database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ShowFirstRunPanel();
        }

        // first run has no accounts, so the right panel becomes the admin signup
        private void ShowFirstRunPanel()
        {
            bool firstRun;
            try
            {
                firstRun = UserService.CountUsers() == 0;
            }
            catch
            {
                return; // database unreachable, keep the sign-in panel
            }

            signupPanel.Visible = firstRun;
            loginPanel.Visible = !firstRun;

            if (firstRun)
            {
                centerLayoutPanel.RowStyles[1].Height = 620;
                AcceptButton = signupButton;
            }
            else
            {
                centerLayoutPanel.RowStyles[1].Height = 400;
                AcceptButton = LoginButton;
            }
        }

        private void SignupButton_Click(object sender, EventArgs e)
        {
            string username = signupUsernameTextBox.Text.Trim();
            string firstName = signupFirstNameTextBox.Text.Trim();
            string lastName = signupLastNameTextBox.Text.Trim();
            string email = signupEmailTextBox.Text.Trim();
            string password = signupPasswordTextBox.Text;
            string confirmPassword = signupConfirmTextBox.Text;

            if (username == "" || firstName == "" || lastName == "" || email == "" || password == "")
            {
                MessageBox.Show("Fill in every field except birthdate.", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains('@'))
            {
                MessageBox.Show("Enter a valid email address.", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < UserService.MinPasswordLength)
            {
                MessageBox.Show("Use at least " + UserService.MinPasswordLength + " characters.", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords do not match.", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? birthdate = null;
            if (signupBirthdatePicker.Checked)
            {
                birthdate = signupBirthdatePicker.Value.Date;
            }

            try
            {
                if (!UserService.AddUser(username, password, email, firstName, lastName, birthdate, "ADMIN", true, ""))
                {
                    MessageBox.Show("That username or email is already taken.", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create the account. " + ex.Message, "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Admin account created. Sign in with it now.", "First run", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowFirstRunPanel();
            usernameTextBox.Text = username;
            passwordTextBox.Text = "";
            passwordTextBox.Focus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string identifier = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            try
            {
                if (AuthService.VerifyAuth(identifier, password))
                {
                    if (!AuthService.IsApproved(identifier))
                    {
                        MessageBox.Show("This account is waiting for admin approval.", "Sign-in blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string role = AuthService.GetRole(identifier) ?? "";
                    if (role != "ADMIN")
                    {
                        MessageBox.Show("Staff accounts can only sign in on the website scanner.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string actualUsername = AuthService.GetUsername(identifier) ?? identifier;
                    Session.SignIn(actualUsername, role);
                    MessageBox.Show("You are signed in. Opening Evently now.", "Signed in", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    usernameTextBox.Text = "";
                    passwordTextBox.Text = "";
                    Hide();
                    using var mainForm = new MainForm();
                    mainForm.ShowDialog(this);
                    Session.SignOut();
                    Show();
                }
                else if (UserService.CountUsers() == 0)
                {
                    MessageBox.Show("No administrator account exists yet. Please create the initial admin account.", "First run", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowFirstRunPanel();
                }
                else { MessageBox.Show("That username and password do not match an active account.", "Sign-in failed", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not sign in. " + ex.Message, "Sign-in error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } // end loginButton_Click

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
        }

        private void SignupShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool show = signupShowPasswordCheckBox.Checked;
            signupPasswordTextBox.UseSystemPasswordChar = !show;
            signupConfirmTextBox.UseSystemPasswordChar = !show;
        }

    } // end class

} // end namespace
