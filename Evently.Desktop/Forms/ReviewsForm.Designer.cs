#nullable disable
namespace Evently.Forms
{
    partial class ReviewsForm
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.eventLabel = new System.Windows.Forms.Label();
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.gridHostPanel = new System.Windows.Forms.Panel();
            this.reviewsDataGridView = new System.Windows.Forms.DataGridView();
            this.attendeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submittedAtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentPreviewPanel = new System.Windows.Forms.Panel();
            this.commentPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.selectedCommentTextBox = new System.Windows.Forms.TextBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.activityLogPanel = new System.Windows.Forms.Panel();
            this.activityLogGroupBox = new System.Windows.Forms.GroupBox();
            this.activityLogLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.rightColumnPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.gridHostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reviewsDataGridView)).BeginInit();
            this.commentPreviewPanel.SuspendLayout();
            this.commentPreviewGroupBox.SuspendLayout();
            this.activityLogPanel.SuspendLayout();
            this.activityLogGroupBox.SuspendLayout();
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
            this.titleLabel.Size = new System.Drawing.Size(175, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Collect Reviews";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // heroSublineLabel
            this.heroSublineLabel.AutoSize = true;
            this.heroSublineLabel.BackColor = System.Drawing.Color.Transparent;
            this.heroSublineLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.heroSublineLabel.ForeColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.heroSublineLabel.Location = new System.Drawing.Point(12, 38);
            this.heroSublineLabel.Name = "heroSublineLabel";
            this.heroSublineLabel.Size = new System.Drawing.Size(465, 15);
            this.heroSublineLabel.TabIndex = 1;
            this.heroSublineLabel.Text = "View ratings and attendee comments submitted on the website.";
            this.heroSublineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // contentPanel
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.gridHostPanel);
            this.contentPanel.Controls.Add(this.rightColumnPanel);
            this.contentPanel.Controls.Add(this.topPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(12);
            this.contentPanel.Size = new System.Drawing.Size(1280, 656);
            this.contentPanel.TabIndex = 1;
            // rightColumnPanel
            this.rightColumnPanel.BackColor = System.Drawing.Color.White;
            this.rightColumnPanel.Controls.Add(this.commentPreviewPanel);
            this.rightColumnPanel.Controls.Add(this.activityLogPanel);
            this.rightColumnPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightColumnPanel.Location = new System.Drawing.Point(808, 52);
            this.rightColumnPanel.Name = "rightColumnPanel";
            this.rightColumnPanel.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.rightColumnPanel.Size = new System.Drawing.Size(460, 592);
            this.rightColumnPanel.TabIndex = 1;
            // topPanel
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.eventLabel);
            this.topPanel.Controls.Add(this.eventComboBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(12, 12);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1256, 40);
            this.topPanel.TabIndex = 0;
            // eventLabel
            this.eventLabel.AutoSize = true;
            this.eventLabel.Location = new System.Drawing.Point(0, 11);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.TabIndex = 0;
            this.eventLabel.Text = "Event:";
            // eventComboBox
            this.eventComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.eventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.Location = new System.Drawing.Point(58, 8);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(1198, 23);
            this.eventComboBox.TabIndex = 1;
            this.eventComboBox.SelectedIndexChanged += new System.EventHandler(this.eventComboBox_SelectedIndexChanged);
            // gridHostPanel
            this.gridHostPanel.BackColor = System.Drawing.Color.White;
            this.gridHostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridHostPanel.Controls.Add(this.reviewsDataGridView);
            this.gridHostPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHostPanel.Location = new System.Drawing.Point(12, 52);
            this.gridHostPanel.Name = "gridHostPanel";
            this.gridHostPanel.Size = new System.Drawing.Size(796, 592);
            this.gridHostPanel.TabIndex = 1;
            // reviewsDataGridView
            this.reviewsDataGridView.AllowUserToAddRows = false;
            this.reviewsDataGridView.AllowUserToDeleteRows = false;
            this.reviewsDataGridView.AllowUserToResizeRows = false;
            this.reviewsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 249, 249);
            this.reviewsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reviewsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.reviewsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.reviewsDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.reviewsDataGridView.ColumnHeadersHeight = 36;
            this.reviewsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.reviewsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.attendeeNameColumn,
            this.ratingColumn,
            this.commentColumn,
            this.submittedAtColumn});
            this.reviewsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.reviewsDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(218, 230, 245);
            this.reviewsDataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.reviewsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reviewsDataGridView.EnableHeadersVisualStyles = false;
            this.reviewsDataGridView.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.reviewsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.reviewsDataGridView.MultiSelect = false;
            this.reviewsDataGridView.Name = "reviewsDataGridView";
            this.reviewsDataGridView.ReadOnly = true;
            this.reviewsDataGridView.RowHeadersVisible = false;
            this.reviewsDataGridView.RowTemplate.Height = 28;
            this.reviewsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.reviewsDataGridView.Size = new System.Drawing.Size(794, 590);
            this.reviewsDataGridView.TabIndex = 0;
            this.reviewsDataGridView.SelectionChanged += new System.EventHandler(this.ReviewsDataGridView_SelectionChanged);
            // attendeeNameColumn
            this.attendeeNameColumn.DataPropertyName = "attendee_name";
            this.attendeeNameColumn.FillWeight = 50F;
            this.attendeeNameColumn.HeaderText = "Attendee";
            this.attendeeNameColumn.Name = "attendeeNameColumn";
            this.attendeeNameColumn.ReadOnly = true;
            // ratingColumn
            this.ratingColumn.DataPropertyName = "rating";
            this.ratingColumn.FillWeight = 20F;
            this.ratingColumn.HeaderText = "Rating";
            this.ratingColumn.Name = "ratingColumn";
            this.ratingColumn.ReadOnly = true;
            // commentColumn
            this.commentColumn.DataPropertyName = "comment";
            this.commentColumn.FillWeight = 110F;
            this.commentColumn.HeaderText = "Comment";
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.ReadOnly = true;
            // submittedAtColumn
            this.submittedAtColumn.DataPropertyName = "submitted_at";
            this.submittedAtColumn.FillWeight = 40F;
            this.submittedAtColumn.HeaderText = "Submitted";
            this.submittedAtColumn.Name = "submittedAtColumn";
            this.submittedAtColumn.ReadOnly = true;
            // commentPreviewPanel
            this.commentPreviewPanel.BackColor = System.Drawing.Color.White;
            this.commentPreviewPanel.Controls.Add(this.commentPreviewGroupBox);
            this.commentPreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentPreviewPanel.Location = new System.Drawing.Point(12, 0);
            this.commentPreviewPanel.Name = "commentPreviewPanel";
            this.commentPreviewPanel.Size = new System.Drawing.Size(448, 496);
            this.commentPreviewPanel.TabIndex = 5;
            // commentPreviewGroupBox
            this.commentPreviewGroupBox.BackColor = System.Drawing.Color.White;
            this.commentPreviewGroupBox.Controls.Add(this.selectedCommentTextBox);
            this.commentPreviewGroupBox.Controls.Add(this.resultLabel);
            this.commentPreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentPreviewGroupBox.Location = new System.Drawing.Point(0, 0);
            this.commentPreviewGroupBox.Name = "commentPreviewGroupBox";
            this.commentPreviewGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.commentPreviewGroupBox.Size = new System.Drawing.Size(448, 496);
            this.commentPreviewGroupBox.TabIndex = 0;
            this.commentPreviewGroupBox.TabStop = false;
            this.commentPreviewGroupBox.Text = "Review Comment Details";
            // selectedCommentTextBox
            this.selectedCommentTextBox.BackColor = System.Drawing.Color.FromArgb(249, 249, 249);
            this.selectedCommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectedCommentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCommentTextBox.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.selectedCommentTextBox.Location = new System.Drawing.Point(8, 24);
            this.selectedCommentTextBox.Multiline = true;
            this.selectedCommentTextBox.Name = "selectedCommentTextBox";
            this.selectedCommentTextBox.ReadOnly = true;
            this.selectedCommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.selectedCommentTextBox.Size = new System.Drawing.Size(432, 438);
            this.selectedCommentTextBox.TabIndex = 0;
            // resultLabel
            this.resultLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultLabel.ForeColor = System.Drawing.Color.FromArgb(95, 95, 95);
            this.resultLabel.Location = new System.Drawing.Point(8, 462);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(432, 26);
            this.resultLabel.TabIndex = 1;
            this.resultLabel.Text = "";
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // activityLogPanel
            this.activityLogPanel.BackColor = System.Drawing.Color.White;
            this.activityLogPanel.Controls.Add(this.activityLogGroupBox);
            this.activityLogPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityLogPanel.Location = new System.Drawing.Point(12, 0);
            this.activityLogPanel.Name = "activityLogPanel";
            this.activityLogPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.activityLogPanel.Size = new System.Drawing.Size(448, 96);
            this.activityLogPanel.TabIndex = 6;
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
            // ReviewsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "ReviewsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collect Reviews";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.rightColumnPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.gridHostPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reviewsDataGridView)).EndInit();
            this.commentPreviewGroupBox.ResumeLayout(false);
            this.commentPreviewGroupBox.PerformLayout();
            this.commentPreviewPanel.ResumeLayout(false);
            this.activityLogGroupBox.ResumeLayout(false);
            this.activityLogGroupBox.PerformLayout();
            this.activityLogPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label heroSublineLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel rightColumnPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.ComboBox eventComboBox;
        private System.Windows.Forms.Panel gridHostPanel;
        private System.Windows.Forms.DataGridView reviewsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn attendeeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn submittedAtColumn;
        private System.Windows.Forms.Panel commentPreviewPanel;
        private System.Windows.Forms.GroupBox commentPreviewGroupBox;
        private System.Windows.Forms.TextBox selectedCommentTextBox;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Panel activityLogPanel;
        private System.Windows.Forms.GroupBox activityLogGroupBox;
        private System.Windows.Forms.Label activityLogLabel;
    }
}
