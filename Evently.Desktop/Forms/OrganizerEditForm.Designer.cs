#nullable disable
namespace Evently.Forms
{
    partial class OrganizerEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.organizationLabel = new System.Windows.Forms.Label();
            this.organizationTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // nameLabel
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(24, 24);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(46, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name:";
            // nameTextBox
            this.nameTextBox.Location = new System.Drawing.Point(120, 21);
            this.nameTextBox.MaxLength = 100;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 23);
            this.nameTextBox.TabIndex = 1;
            // emailLabel
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(24, 64);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(42, 17);
            this.emailLabel.TabIndex = 2;
            this.emailLabel.Text = "Email:";
            // emailTextBox
            this.emailTextBox.Location = new System.Drawing.Point(120, 61);
            this.emailTextBox.MaxLength = 100;
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(260, 23);
            this.emailTextBox.TabIndex = 3;
            // phoneLabel
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(24, 104);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(47, 17);
            this.phoneLabel.TabIndex = 4;
            this.phoneLabel.Text = "Phone:";
            // phoneTextBox
            this.phoneTextBox.Location = new System.Drawing.Point(120, 101);
            this.phoneTextBox.MaxLength = 30;
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(260, 23);
            this.phoneTextBox.TabIndex = 5;
            // organizationLabel
            this.organizationLabel.AutoSize = true;
            this.organizationLabel.Location = new System.Drawing.Point(24, 144);
            this.organizationLabel.Name = "organizationLabel";
            this.organizationLabel.Size = new System.Drawing.Size(86, 17);
            this.organizationLabel.TabIndex = 6;
            this.organizationLabel.Text = "Organization:";
            // organizationTextBox
            this.organizationTextBox.Location = new System.Drawing.Point(120, 141);
            this.organizationTextBox.MaxLength = 150;
            this.organizationTextBox.Name = "organizationTextBox";
            this.organizationTextBox.Size = new System.Drawing.Size(260, 23);
            this.organizationTextBox.TabIndex = 7;
            // saveButton
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(210, 185);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 30);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // cancelButton
            this.cancelButton.Location = new System.Drawing.Point(300, 185);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 30);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // OrganizerEditForm
            this.AcceptButton = this.saveButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(414, 235);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.organizationLabel);
            this.Controls.Add(this.organizationTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrganizerEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Event Manager";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Label organizationLabel;
        private System.Windows.Forms.TextBox organizationTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
