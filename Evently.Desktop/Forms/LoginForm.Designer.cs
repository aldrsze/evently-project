#nullable disable
namespace Evently.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.heroSublineLabel = new System.Windows.Forms.Label();
            this.benefit1Label = new System.Windows.Forms.Label();
            this.benefit2Label = new System.Windows.Forms.Label();
            this.benefit3Label = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.centerLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.formTitleLabel = new System.Windows.Forms.Label();
            this.formSubLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.loginHintLabel = new System.Windows.Forms.Label();
            this.reassuranceLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.signupPanel = new System.Windows.Forms.Panel();
            this.signupTitleLabel = new System.Windows.Forms.Label();
            this.signupSubLabel = new System.Windows.Forms.Label();
            this.signupUsernameLabel = new System.Windows.Forms.Label();
            this.signupUsernameTextBox = new System.Windows.Forms.TextBox();
            this.signupFirstNameLabel = new System.Windows.Forms.Label();
            this.signupFirstNameTextBox = new System.Windows.Forms.TextBox();
            this.signupLastNameLabel = new System.Windows.Forms.Label();
            this.signupLastNameTextBox = new System.Windows.Forms.TextBox();
            this.signupEmailLabel = new System.Windows.Forms.Label();
            this.signupEmailTextBox = new System.Windows.Forms.TextBox();
            this.signupBirthdateLabel = new System.Windows.Forms.Label();
            this.signupBirthdatePicker = new System.Windows.Forms.DateTimePicker();
            this.signupPasswordLabel = new System.Windows.Forms.Label();
            this.signupPasswordTextBox = new System.Windows.Forms.TextBox();
            this.signupConfirmLabel = new System.Windows.Forms.Label();
            this.signupConfirmTextBox = new System.Windows.Forms.TextBox();
            this.signupButton = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.centerLayoutPanel.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.signupPanel.SuspendLayout();
            this.SuspendLayout();
            // headerPanel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.heroSublineLabel);
            this.headerPanel.Controls.Add(this.benefit1Label);
            this.headerPanel.Controls.Add(this.benefit2Label);
            this.headerPanel.Controls.Add(this.benefit3Label);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(440, 720);
            this.headerPanel.TabIndex = 0;
            // titleLabel
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(48, 136);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(148, 51);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Evently";
            // heroSublineLabel
            this.heroSublineLabel.AutoSize = false;
            this.heroSublineLabel.BackColor = System.Drawing.Color.Transparent;
            this.heroSublineLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.heroSublineLabel.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.heroSublineLabel.Location = new System.Drawing.Point(48, 204);
            this.heroSublineLabel.Name = "heroSublineLabel";
            this.heroSublineLabel.Size = new System.Drawing.Size(344, 74);
            this.heroSublineLabel.TabIndex = 1;
            this.heroSublineLabel.Text = "Register attendees, issue QR tickets, and monitor venue capacity in real time.";
            // benefit1Label
            this.benefit1Label.AutoSize = true;
            this.benefit1Label.BackColor = System.Drawing.Color.Transparent;
            this.benefit1Label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.benefit1Label.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.benefit1Label.Location = new System.Drawing.Point(48, 304);
            this.benefit1Label.Name = "benefit1Label";
            this.benefit1Label.Size = new System.Drawing.Size(0, 17);
            this.benefit1Label.TabIndex = 2;
            this.benefit1Label.Text = "•  Register attendees in a few clicks";
            // benefit2Label
            this.benefit2Label.AutoSize = true;
            this.benefit2Label.BackColor = System.Drawing.Color.Transparent;
            this.benefit2Label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.benefit2Label.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.benefit2Label.Location = new System.Drawing.Point(48, 340);
            this.benefit2Label.Name = "benefit2Label";
            this.benefit2Label.Size = new System.Drawing.Size(0, 17);
            this.benefit2Label.TabIndex = 3;
            this.benefit2Label.Text = "•  Issue QR tickets at the door";
            // benefit3Label
            this.benefit3Label.AutoSize = true;
            this.benefit3Label.BackColor = System.Drawing.Color.Transparent;
            this.benefit3Label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.benefit3Label.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.benefit3Label.Location = new System.Drawing.Point(48, 376);
            this.benefit3Label.Name = "benefit3Label";
            this.benefit3Label.Size = new System.Drawing.Size(0, 17);
            this.benefit3Label.TabIndex = 4;
            this.benefit3Label.Text = "•  Watch live capacity as attendees arrive";
            // contentPanel
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(249, 249, 249);
            this.contentPanel.Controls.Add(this.centerLayoutPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(440, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(840, 720);
            this.contentPanel.TabIndex = 1;
            // centerLayoutPanel
            this.centerLayoutPanel.ColumnCount = 3;
            this.centerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.centerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.centerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.centerLayoutPanel.Controls.Add(this.loginPanel, 1, 1);
            this.centerLayoutPanel.Controls.Add(this.signupPanel, 1, 1);
            this.centerLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.centerLayoutPanel.Name = "centerLayoutPanel";
            this.centerLayoutPanel.RowCount = 3;
            this.centerLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.centerLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.centerLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.centerLayoutPanel.Size = new System.Drawing.Size(840, 720);
            this.centerLayoutPanel.TabIndex = 0;
            // loginPanel
            this.loginPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginPanel.BackColor = System.Drawing.Color.White;
            this.loginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginPanel.Controls.Add(this.formTitleLabel);
            this.loginPanel.Controls.Add(this.formSubLabel);
            this.loginPanel.Controls.Add(this.usernameLabel);
            this.loginPanel.Controls.Add(this.usernameTextBox);
            this.loginPanel.Controls.Add(this.passwordLabel);
            this.loginPanel.Controls.Add(this.passwordTextBox);
            this.loginPanel.Controls.Add(this.errorLabel);
            this.loginPanel.Controls.Add(this.loginHintLabel);
            this.loginPanel.Controls.Add(this.reassuranceLabel);
            this.loginPanel.Controls.Add(this.LoginButton);
            this.loginPanel.Location = new System.Drawing.Point(210, 400);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(420, 400);
            this.loginPanel.TabIndex = 0;
            // formTitleLabel
            this.formTitleLabel.AutoSize = true;
            this.formTitleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.formTitleLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.formTitleLabel.Location = new System.Drawing.Point(40, 28);
            this.formTitleLabel.Name = "formTitleLabel";
            this.formTitleLabel.Size = new System.Drawing.Size(78, 30);
            this.formTitleLabel.TabIndex = 8;
            this.formTitleLabel.Text = "Sign in";
            // formSubLabel
            this.formSubLabel.AutoSize = true;
            this.formSubLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.formSubLabel.ForeColor = System.Drawing.Color.FromArgb(95, 95, 95);
            this.formSubLabel.Location = new System.Drawing.Point(40, 70);
            this.formSubLabel.Name = "formSubLabel";
            this.formSubLabel.Size = new System.Drawing.Size(0, 15);
            this.formSubLabel.TabIndex = 9;
            this.formSubLabel.Text = "Use the staff account you signed up with";
            // usernameLabel
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(40, 108);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(63, 15);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username or Email:";
            // usernameTextBox
            this.usernameTextBox.Location = new System.Drawing.Point(40, 130);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(340, 23);
            this.usernameTextBox.TabIndex = 1;
            // passwordLabel
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(40, 170);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(60, 15);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password:";
            // passwordTextBox
            this.passwordTextBox.Location = new System.Drawing.Point(40, 192);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(340, 23);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // errorLabel
            this.errorLabel.AutoSize = false;
            this.errorLabel.Location = new System.Drawing.Point(40, 226);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(340, 22);
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "";
            // loginHintLabel
            this.loginHintLabel.AutoSize = false;
            this.loginHintLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.loginHintLabel.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.loginHintLabel.Location = new System.Drawing.Point(40, 252);
            this.loginHintLabel.Name = "loginHintLabel";
            this.loginHintLabel.Size = new System.Drawing.Size(340, 34);
            this.loginHintLabel.TabIndex = 6;
            this.loginHintLabel.Text = "New staff accounts need admin approval first";
            // reassuranceLabel
            this.reassuranceLabel.AutoSize = true;
            this.reassuranceLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reassuranceLabel.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.reassuranceLabel.Location = new System.Drawing.Point(40, 356);
            this.reassuranceLabel.Name = "reassuranceLabel";
            this.reassuranceLabel.Size = new System.Drawing.Size(222, 13);
            this.reassuranceLabel.TabIndex = 7;
            this.reassuranceLabel.Text = "Data stays stored in your local database";
            // loginButton
            this.LoginButton.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.LoginButton.FlatAppearance.BorderSize = 0;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Location = new System.Drawing.Point(260, 300);
            this.LoginButton.Name = "loginButton";
            this.LoginButton.Size = new System.Drawing.Size(120, 36);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "&Sign in";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // signupPanel
            this.signupPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.signupPanel.BackColor = System.Drawing.Color.White;
            this.signupPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signupPanel.Controls.Add(this.signupTitleLabel);
            this.signupPanel.Controls.Add(this.signupSubLabel);
            this.signupPanel.Controls.Add(this.signupUsernameLabel);
            this.signupPanel.Controls.Add(this.signupUsernameTextBox);
            this.signupPanel.Controls.Add(this.signupFirstNameLabel);
            this.signupPanel.Controls.Add(this.signupFirstNameTextBox);
            this.signupPanel.Controls.Add(this.signupLastNameLabel);
            this.signupPanel.Controls.Add(this.signupLastNameTextBox);
            this.signupPanel.Controls.Add(this.signupEmailLabel);
            this.signupPanel.Controls.Add(this.signupEmailTextBox);
            this.signupPanel.Controls.Add(this.signupBirthdateLabel);
            this.signupPanel.Controls.Add(this.signupBirthdatePicker);
            this.signupPanel.Controls.Add(this.signupPasswordLabel);
            this.signupPanel.Controls.Add(this.signupPasswordTextBox);
            this.signupPanel.Controls.Add(this.signupConfirmLabel);
            this.signupPanel.Controls.Add(this.signupConfirmTextBox);
            this.signupPanel.Controls.Add(this.signupButton);
            this.signupPanel.Location = new System.Drawing.Point(210, 50);
            this.signupPanel.Name = "signupPanel";
            this.signupPanel.Size = new System.Drawing.Size(420, 620);
            this.signupPanel.TabIndex = 1;
            this.signupPanel.Visible = false;
            // signupTitleLabel
            this.signupTitleLabel.AutoSize = true;
            this.signupTitleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.signupTitleLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.signupTitleLabel.Location = new System.Drawing.Point(40, 28);
            this.signupTitleLabel.Name = "signupTitleLabel";
            this.signupTitleLabel.Size = new System.Drawing.Size(233, 30);
            this.signupTitleLabel.TabIndex = 0;
            this.signupTitleLabel.Text = "Create admin account";
            // signupSubLabel
            this.signupSubLabel.AutoSize = true;
            this.signupSubLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.signupSubLabel.ForeColor = System.Drawing.Color.FromArgb(95, 95, 95);
            this.signupSubLabel.Location = new System.Drawing.Point(40, 70);
            this.signupSubLabel.Name = "signupSubLabel";
            this.signupSubLabel.Size = new System.Drawing.Size(0, 15);
            this.signupSubLabel.TabIndex = 1;
            this.signupSubLabel.Text = "No accounts yet. This first account runs Evently.";
            // signupUsernameLabel
            this.signupUsernameLabel.AutoSize = true;
            this.signupUsernameLabel.Location = new System.Drawing.Point(40, 108);
            this.signupUsernameLabel.Name = "signupUsernameLabel";
            this.signupUsernameLabel.Size = new System.Drawing.Size(63, 15);
            this.signupUsernameLabel.TabIndex = 2;
            this.signupUsernameLabel.Text = "Username:";
            // signupUsernameTextBox
            this.signupUsernameTextBox.Location = new System.Drawing.Point(40, 130);
            this.signupUsernameTextBox.Name = "signupUsernameTextBox";
            this.signupUsernameTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupUsernameTextBox.TabIndex = 3;
            // signupFirstNameLabel
            this.signupFirstNameLabel.AutoSize = true;
            this.signupFirstNameLabel.Location = new System.Drawing.Point(40, 170);
            this.signupFirstNameLabel.Name = "signupFirstNameLabel";
            this.signupFirstNameLabel.Size = new System.Drawing.Size(65, 15);
            this.signupFirstNameLabel.TabIndex = 4;
            this.signupFirstNameLabel.Text = "First name:";
            // signupFirstNameTextBox
            this.signupFirstNameTextBox.Location = new System.Drawing.Point(40, 192);
            this.signupFirstNameTextBox.Name = "signupFirstNameTextBox";
            this.signupFirstNameTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupFirstNameTextBox.TabIndex = 5;
            // signupLastNameLabel
            this.signupLastNameLabel.AutoSize = true;
            this.signupLastNameLabel.Location = new System.Drawing.Point(40, 232);
            this.signupLastNameLabel.Name = "signupLastNameLabel";
            this.signupLastNameLabel.Size = new System.Drawing.Size(65, 15);
            this.signupLastNameLabel.TabIndex = 6;
            this.signupLastNameLabel.Text = "Last name:";
            // signupLastNameTextBox
            this.signupLastNameTextBox.Location = new System.Drawing.Point(40, 254);
            this.signupLastNameTextBox.Name = "signupLastNameTextBox";
            this.signupLastNameTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupLastNameTextBox.TabIndex = 7;
            // signupEmailLabel
            this.signupEmailLabel.AutoSize = true;
            this.signupEmailLabel.Location = new System.Drawing.Point(40, 294);
            this.signupEmailLabel.Name = "signupEmailLabel";
            this.signupEmailLabel.Size = new System.Drawing.Size(44, 15);
            this.signupEmailLabel.TabIndex = 8;
            this.signupEmailLabel.Text = "Email:";
            // signupEmailTextBox
            this.signupEmailTextBox.Location = new System.Drawing.Point(40, 316);
            this.signupEmailTextBox.Name = "signupEmailTextBox";
            this.signupEmailTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupEmailTextBox.TabIndex = 9;
            // signupBirthdateLabel
            this.signupBirthdateLabel.AutoSize = true;
            this.signupBirthdateLabel.Location = new System.Drawing.Point(40, 356);
            this.signupBirthdateLabel.Name = "signupBirthdateLabel";
            this.signupBirthdateLabel.Size = new System.Drawing.Size(106, 15);
            this.signupBirthdateLabel.TabIndex = 10;
            this.signupBirthdateLabel.Text = "Birthdate (optional):";
            // signupBirthdatePicker
            this.signupBirthdatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.signupBirthdatePicker.Location = new System.Drawing.Point(40, 378);
            this.signupBirthdatePicker.Name = "signupBirthdatePicker";
            this.signupBirthdatePicker.ShowCheckBox = true;
            this.signupBirthdatePicker.Size = new System.Drawing.Size(340, 23);
            this.signupBirthdatePicker.TabIndex = 11;
            // signupPasswordLabel
            this.signupPasswordLabel.AutoSize = true;
            this.signupPasswordLabel.Location = new System.Drawing.Point(40, 418);
            this.signupPasswordLabel.Name = "signupPasswordLabel";
            this.signupPasswordLabel.Size = new System.Drawing.Size(60, 15);
            this.signupPasswordLabel.TabIndex = 12;
            this.signupPasswordLabel.Text = "Password:";
            // signupPasswordTextBox
            this.signupPasswordTextBox.Location = new System.Drawing.Point(40, 440);
            this.signupPasswordTextBox.Name = "signupPasswordTextBox";
            this.signupPasswordTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupPasswordTextBox.TabIndex = 13;
            this.signupPasswordTextBox.UseSystemPasswordChar = true;
            // signupConfirmLabel
            this.signupConfirmLabel.AutoSize = true;
            this.signupConfirmLabel.Location = new System.Drawing.Point(40, 480);
            this.signupConfirmLabel.Name = "signupConfirmLabel";
            this.signupConfirmLabel.Size = new System.Drawing.Size(107, 15);
            this.signupConfirmLabel.TabIndex = 14;
            this.signupConfirmLabel.Text = "Confirm password:";
            // signupConfirmTextBox
            this.signupConfirmTextBox.Location = new System.Drawing.Point(40, 502);
            this.signupConfirmTextBox.Name = "signupConfirmTextBox";
            this.signupConfirmTextBox.Size = new System.Drawing.Size(340, 23);
            this.signupConfirmTextBox.TabIndex = 15;
            this.signupConfirmTextBox.UseSystemPasswordChar = true;
            // signupButton
            this.signupButton.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.signupButton.FlatAppearance.BorderSize = 0;
            this.signupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signupButton.ForeColor = System.Drawing.Color.White;
            this.signupButton.Location = new System.Drawing.Point(260, 566);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(120, 36);
            this.signupButton.TabIndex = 17;
            this.signupButton.Text = "&Create account";
            this.signupButton.UseVisualStyleBackColor = false;
            this.signupButton.Click += new System.EventHandler(this.SignupButton_Click);
            // LoginForm
            this.AcceptButton = this.LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Evently - Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.centerLayoutPanel.ResumeLayout(false);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.signupPanel.ResumeLayout(false);
            this.signupPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label heroSublineLabel;
        private System.Windows.Forms.Label benefit1Label;
        private System.Windows.Forms.Label benefit2Label;
        private System.Windows.Forms.Label benefit3Label;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.TableLayoutPanel centerLayoutPanel;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label formTitleLabel;
        private System.Windows.Forms.Label formSubLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label loginHintLabel;
        private System.Windows.Forms.Label reassuranceLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Panel signupPanel;
        private System.Windows.Forms.Label signupTitleLabel;
        private System.Windows.Forms.Label signupSubLabel;
        private System.Windows.Forms.Label signupUsernameLabel;
        private System.Windows.Forms.TextBox signupUsernameTextBox;
        private System.Windows.Forms.Label signupFirstNameLabel;
        private System.Windows.Forms.TextBox signupFirstNameTextBox;
        private System.Windows.Forms.Label signupLastNameLabel;
        private System.Windows.Forms.TextBox signupLastNameTextBox;
        private System.Windows.Forms.Label signupEmailLabel;
        private System.Windows.Forms.TextBox signupEmailTextBox;
        private System.Windows.Forms.Label signupBirthdateLabel;
        private System.Windows.Forms.DateTimePicker signupBirthdatePicker;
        private System.Windows.Forms.Label signupPasswordLabel;
        private System.Windows.Forms.TextBox signupPasswordTextBox;
        private System.Windows.Forms.Label signupConfirmLabel;
        private System.Windows.Forms.TextBox signupConfirmTextBox;
        private System.Windows.Forms.Button signupButton;
    }
}
