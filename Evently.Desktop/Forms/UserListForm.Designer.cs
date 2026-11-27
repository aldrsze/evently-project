#nullable disable
namespace Evently.Forms
{
    partial class UserListForm
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
            this.contentPanel = new System.Windows.Forms.Panel();
            this.rightColumnPanel = new System.Windows.Forms.Panel();
            this.gridHostPanel = new System.Windows.Forms.Panel();
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.usernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addUserWrapperPanel = new System.Windows.Forms.Panel();
            this.addUserGroupBox = new System.Windows.Forms.GroupBox();
            this.guidancePanel = new System.Windows.Forms.Panel();
            this.guidanceGroupBox = new System.Windows.Forms.GroupBox();
            this.activityLogPanel = new System.Windows.Forms.Panel();
            this.activityLogGroupBox = new System.Windows.Forms.GroupBox();
            this.activityLogLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.roleLabel = new System.Windows.Forms.Label();
            this.roleComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.actionPanel = new System.Windows.Forms.Panel();
            this.accountStatusGuidanceLabel = new System.Windows.Forms.Label();
            this.approveButton = new System.Windows.Forms.Button();
            this.deactivateButton = new System.Windows.Forms.Button();
            this.reactivateButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.rightColumnPanel.SuspendLayout();
            this.gridHostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            this.addUserWrapperPanel.SuspendLayout();
            this.addUserGroupBox.SuspendLayout();
            this.guidancePanel.SuspendLayout();
            this.guidanceGroupBox.SuspendLayout();
            this.activityLogPanel.SuspendLayout();
            this.activityLogGroupBox.SuspendLayout();
            this.actionPanel.SuspendLayout();
            this.SuspendLayout();
            // headerPanel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.heroSublineLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.headerPanel.Size = new System.Drawing.Size(1280, 64);
            this.headerPanel.TabIndex = 0;
            // titleLabel
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(12, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(138, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Manage Users";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // heroSublineLabel
            this.heroSublineLabel.AutoSize = true;
            this.heroSublineLabel.BackColor = System.Drawing.Color.Transparent;
            this.heroSublineLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.heroSublineLabel.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.heroSublineLabel.Location = new System.Drawing.Point(12, 38);
            this.heroSublineLabel.Name = "heroSublineLabel";
            this.heroSublineLabel.Size = new System.Drawing.Size(294, 15);
            this.heroSublineLabel.TabIndex = 1;
            this.heroSublineLabel.Text = "Configure administrator and staff access accounts.";
            this.heroSublineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // contentPanel
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.gridHostPanel);
            this.contentPanel.Controls.Add(this.rightColumnPanel);
            this.contentPanel.Controls.Add(this.actionPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(12);
            this.contentPanel.Size = new System.Drawing.Size(1280, 656);
            this.contentPanel.TabIndex = 1;
            // rightColumnPanel
            this.rightColumnPanel.BackColor = System.Drawing.Color.White;
            this.rightColumnPanel.Controls.Add(this.addUserWrapperPanel);
            this.rightColumnPanel.Controls.Add(this.guidancePanel);
            this.rightColumnPanel.Controls.Add(this.activityLogPanel);
            this.rightColumnPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightColumnPanel.Location = new System.Drawing.Point(808, 12);
            this.rightColumnPanel.Name = "rightColumnPanel";
            this.rightColumnPanel.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.rightColumnPanel.Size = new System.Drawing.Size(460, 578);
            this.rightColumnPanel.TabIndex = 1;
            // gridHostPanel
            this.gridHostPanel.BackColor = System.Drawing.Color.White;
            this.gridHostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridHostPanel.Controls.Add(this.usersDataGridView);
            this.gridHostPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHostPanel.Location = new System.Drawing.Point(12, 12);
            this.gridHostPanel.Name = "gridHostPanel";
            this.gridHostPanel.Size = new System.Drawing.Size(796, 578);
            this.gridHostPanel.TabIndex = 0;
            // usersDataGridView
            this.usersDataGridView.AllowUserToAddRows = false;
            this.usersDataGridView.AllowUserToDeleteRows = false;
            this.usersDataGridView.AllowUserToResizeRows = false;
            this.usersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.usersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.usersDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.usersDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.usersDataGridView.ColumnHeadersHeight = 36;
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.usersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usernameColumn,
            this.fullNameColumn,
            this.emailColumn,
            this.birthdateColumn,
            this.roleColumn,
            this.statusColumn});
            this.usersDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.usersDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.usersDataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.usersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersDataGridView.EnableHeadersVisualStyles = false;
            this.usersDataGridView.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.usersDataGridView.Location = new System.Drawing.Point(0, 0);
            this.usersDataGridView.MultiSelect = false;
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.AutoGenerateColumns = false;
            this.usersDataGridView.ReadOnly = true;
            this.usersDataGridView.RowHeadersVisible = false;
            this.usersDataGridView.RowTemplate.Height = 28;
            this.usersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGridView.Size = new System.Drawing.Size(794, 576);
            this.usersDataGridView.TabIndex = 0;
            // usernameColumn
            this.usernameColumn.HeaderText = "Username";
            this.usernameColumn.Name = "usernameColumn";
            this.usernameColumn.ReadOnly = true;
            this.usernameColumn.DataPropertyName = "username";
            // fullNameColumn
            this.fullNameColumn.HeaderText = "Name";
            this.fullNameColumn.Name = "fullNameColumn";
            this.fullNameColumn.ReadOnly = true;
            this.fullNameColumn.DataPropertyName = "full_name";
            // emailColumn
            this.emailColumn.HeaderText = "Email";
            this.emailColumn.Name = "emailColumn";
            this.emailColumn.ReadOnly = true;
            this.emailColumn.DataPropertyName = "email";
            // birthdateColumn
            this.birthdateColumn.HeaderText = "Birthdate";
            this.birthdateColumn.Name = "birthdateColumn";
            this.birthdateColumn.ReadOnly = true;
            this.birthdateColumn.DataPropertyName = "birthdate";
            // roleColumn
            this.roleColumn.HeaderText = "Role";
            this.roleColumn.Name = "roleColumn";
            this.roleColumn.ReadOnly = true;
            this.roleColumn.DataPropertyName = "role";
            // statusColumn
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.DataPropertyName = "status";
            // addUserWrapperPanel
            this.addUserWrapperPanel.BackColor = System.Drawing.Color.White;
            this.addUserWrapperPanel.Controls.Add(this.addUserGroupBox);
            this.addUserWrapperPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUserWrapperPanel.Location = new System.Drawing.Point(12, 190);
            this.addUserWrapperPanel.Name = "addUserWrapperPanel";
            this.addUserWrapperPanel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.addUserWrapperPanel.Size = new System.Drawing.Size(448, 292);
            this.addUserWrapperPanel.TabIndex = 1;
            // addUserGroupBox
            this.addUserGroupBox.BackColor = System.Drawing.Color.White;
            this.addUserGroupBox.Controls.Add(this.usernameLabel);
            this.addUserGroupBox.Controls.Add(this.usernameTextBox);
            this.addUserGroupBox.Controls.Add(this.firstNameLabel);
            this.addUserGroupBox.Controls.Add(this.firstNameTextBox);
            this.addUserGroupBox.Controls.Add(this.lastNameLabel);
            this.addUserGroupBox.Controls.Add(this.lastNameTextBox);
            this.addUserGroupBox.Controls.Add(this.emailLabel);
            this.addUserGroupBox.Controls.Add(this.emailTextBox);
            this.addUserGroupBox.Controls.Add(this.passwordLabel);
            this.addUserGroupBox.Controls.Add(this.passwordTextBox);
            this.addUserGroupBox.Controls.Add(this.roleLabel);
            this.addUserGroupBox.Controls.Add(this.roleComboBox);
            this.addUserGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUserGroupBox.Location = new System.Drawing.Point(0, 12);
            this.addUserGroupBox.Name = "addUserGroupBox";
            this.addUserGroupBox.Size = new System.Drawing.Size(448, 280);
            this.addUserGroupBox.TabIndex = 0;
            this.addUserGroupBox.TabStop = false;
            this.addUserGroupBox.Text = "Add User";
            // usernameLabel
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(12, 29);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // usernameTextBox
            this.usernameTextBox.Location = new System.Drawing.Point(94, 26);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(130, 23);
            this.usernameTextBox.TabIndex = 1;
            // firstNameLabel
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(240, 29);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.TabIndex = 2;
            this.firstNameLabel.Text = "First name:";
            // firstNameTextBox
            this.firstNameTextBox.Location = new System.Drawing.Point(318, 26);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(118, 23);
            this.firstNameTextBox.TabIndex = 3;
            // lastNameLabel
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(12, 65);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.TabIndex = 4;
            this.lastNameLabel.Text = "Last name:";
            // lastNameTextBox
            this.lastNameTextBox.Location = new System.Drawing.Point(94, 62);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(130, 23);
            this.lastNameTextBox.TabIndex = 5;
            // emailLabel
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(240, 65);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.TabIndex = 6;
            this.emailLabel.Text = "Email:";
            // emailTextBox
            this.emailTextBox.Location = new System.Drawing.Point(318, 62);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(118, 23);
            this.emailTextBox.TabIndex = 7;
            // passwordLabel
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 101);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.TabIndex = 8;
            this.passwordLabel.Text = "Password:";
            // passwordTextBox
            this.passwordTextBox.Location = new System.Drawing.Point(94, 98);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(130, 23);
            this.passwordTextBox.TabIndex = 9;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // roleLabel
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(240, 101);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.TabIndex = 10;
            this.roleLabel.Text = "Role:";
            // roleComboBox
            this.roleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleComboBox.FormattingEnabled = true;
            this.roleComboBox.Items.AddRange(new object[] { "ADMIN", "STAFF" });
            this.roleComboBox.Location = new System.Drawing.Point(318, 98);
            this.roleComboBox.Name = "roleComboBox";
            this.roleComboBox.SelectedIndex = 0;
            this.roleComboBox.Size = new System.Drawing.Size(118, 23);
            this.roleComboBox.TabIndex = 11;
            // guidancePanel
            this.guidancePanel.BackColor = System.Drawing.Color.White;
            this.guidancePanel.Controls.Add(this.guidanceGroupBox);
            this.guidancePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.guidancePanel.Location = new System.Drawing.Point(12, 0);
            this.guidancePanel.Name = "guidancePanel";
            this.guidancePanel.Size = new System.Drawing.Size(448, 190);
            this.guidancePanel.TabIndex = 3;
            // guidanceGroupBox
            this.guidanceGroupBox.BackColor = System.Drawing.Color.White;
            this.guidanceGroupBox.Controls.Add(this.accountStatusGuidanceLabel);
            this.guidanceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guidanceGroupBox.Location = new System.Drawing.Point(0, 0);
            this.guidanceGroupBox.Name = "guidanceGroupBox";
            this.guidanceGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.guidanceGroupBox.Size = new System.Drawing.Size(448, 190);
            this.guidanceGroupBox.TabIndex = 0;
            this.guidanceGroupBox.TabStop = false;
            this.guidanceGroupBox.Text = "Account Status Notes";
            // accountStatusGuidanceLabel
            this.accountStatusGuidanceLabel.AutoSize = false;
            this.accountStatusGuidanceLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.accountStatusGuidanceLabel.ForeColor = System.Drawing.Color.FromArgb(95, 95, 95);
            this.accountStatusGuidanceLabel.Location = new System.Drawing.Point(8, 24);
            this.accountStatusGuidanceLabel.Name = "accountStatusGuidanceLabel";
            this.accountStatusGuidanceLabel.Size = new System.Drawing.Size(432, 150);
            this.accountStatusGuidanceLabel.TabIndex = 3;
            this.accountStatusGuidanceLabel.Text = "Approve new signups, or deactivate accounts that should not sign in.";
            this.accountStatusGuidanceLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // activityLogPanel
            this.activityLogPanel.BackColor = System.Drawing.Color.White;
            this.activityLogPanel.Controls.Add(this.activityLogGroupBox);
            this.activityLogPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityLogPanel.Location = new System.Drawing.Point(12, 0);
            this.activityLogPanel.Name = "activityLogPanel";
            this.activityLogPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.activityLogPanel.Size = new System.Drawing.Size(448, 96);
            this.activityLogPanel.TabIndex = 4;
            // activityLogGroupBox
            this.activityLogGroupBox.BackColor = System.Drawing.Color.White;
            this.activityLogGroupBox.Controls.Add(this.activityLogLabel);
            this.activityLogGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityLogGroupBox.Location = new System.Drawing.Point(0, 0);
            this.activityLogGroupBox.Name = "activityLogGroupBox";
            this.activityLogGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.activityLogGroupBox.Size = new System.Drawing.Size(448, 88);
            this.activityLogGroupBox.TabIndex = 0;
            this.activityLogGroupBox.TabStop = false;
            this.activityLogGroupBox.Text = "Activity Log";
            // activityLogLabel
            this.activityLogLabel.AutoSize = false;
            this.activityLogLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityLogLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.activityLogLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.activityLogLabel.Location = new System.Drawing.Point(8, 24);
            this.activityLogLabel.Name = "activityLogLabel";
            this.activityLogLabel.Size = new System.Drawing.Size(432, 56);
            this.activityLogLabel.TabIndex = 0;
            this.activityLogLabel.Text = "No activity yet.";
            this.activityLogLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // actionPanel
            this.actionPanel.BackColor = System.Drawing.Color.White;
            this.actionPanel.Controls.Add(this.addButton);
            this.actionPanel.Controls.Add(this.approveButton);
            this.actionPanel.Controls.Add(this.editButton);
            this.actionPanel.Controls.Add(this.deactivateButton);
            this.actionPanel.Controls.Add(this.reactivateButton);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.actionPanel.Location = new System.Drawing.Point(12, 590);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new System.Drawing.Size(1256, 54);
            this.actionPanel.TabIndex = 2;
            // addButton
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.addButton.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(774, 16);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(90, 30);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "&Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // approveButton
            this.approveButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.approveButton.Location = new System.Drawing.Point(872, 16);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(90, 30);
            this.approveButton.TabIndex = 2;
            this.approveButton.Text = "A&pprove";
            this.approveButton.UseVisualStyleBackColor = true;
            this.approveButton.Click += new System.EventHandler(this.ApproveButton_Click);
            // deactivateButton
            this.deactivateButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.deactivateButton.Location = new System.Drawing.Point(1068, 16);
            this.deactivateButton.Name = "deactivateButton";
            this.deactivateButton.Size = new System.Drawing.Size(90, 30);
            this.deactivateButton.TabIndex = 0;
            this.deactivateButton.Text = "&Deactivate";
            this.deactivateButton.UseVisualStyleBackColor = true;
            this.deactivateButton.Click += new System.EventHandler(this.DeactivateButton_Click);
            // reactivateButton
            this.reactivateButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.reactivateButton.Location = new System.Drawing.Point(1166, 16);
            this.reactivateButton.Name = "reactivateButton";
            this.reactivateButton.Size = new System.Drawing.Size(90, 30);
            this.reactivateButton.TabIndex = 1;
            this.reactivateButton.Text = "&Reactivate";
            this.reactivateButton.UseVisualStyleBackColor = true;
            this.reactivateButton.Click += new System.EventHandler(this.ReactivateButton_Click);
            // editButton
            this.editButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.editButton.Location = new System.Drawing.Point(970, 16);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(90, 30);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "&Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // UserListForm
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "UserListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.rightColumnPanel.ResumeLayout(false);
            this.gridHostPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            this.addUserWrapperPanel.ResumeLayout(false);
            this.addUserGroupBox.ResumeLayout(false);
            this.addUserGroupBox.PerformLayout();
            this.guidancePanel.ResumeLayout(false);
            this.guidanceGroupBox.ResumeLayout(false);
            this.guidanceGroupBox.PerformLayout();
            this.activityLogGroupBox.ResumeLayout(false);
            this.activityLogGroupBox.PerformLayout();
            this.activityLogPanel.ResumeLayout(false);
            this.actionPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel rightColumnPanel;
        private System.Windows.Forms.Panel gridHostPanel;
        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.Panel addUserWrapperPanel;
        private System.Windows.Forms.GroupBox addUserGroupBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.ComboBox roleComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Panel guidancePanel;
        private System.Windows.Forms.GroupBox guidanceGroupBox;
        private System.Windows.Forms.Panel activityLogPanel;
        private System.Windows.Forms.GroupBox activityLogGroupBox;
        private System.Windows.Forms.Label activityLogLabel;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Label accountStatusGuidanceLabel;
        private System.Windows.Forms.Button approveButton;
        private System.Windows.Forms.Button deactivateButton;
        private System.Windows.Forms.Button reactivateButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Label heroSublineLabel;
    }
}
