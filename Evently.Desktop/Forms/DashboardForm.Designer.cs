#nullable disable
namespace Evently.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea columnChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend columnLegend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series columnSeries = new System.Windows.Forms.DataVisualization.Charting.Series();

            System.Windows.Forms.DataVisualization.Charting.ChartArea doughnutChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend doughnutLegend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series doughnutSeries = new System.Windows.Forms.DataVisualization.Charting.Series();

            System.Windows.Forms.DataVisualization.Charting.ChartArea barChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend barLegend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series barSeries = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.contentPanel = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.eventLabel = new System.Windows.Forms.Label();
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.lastUpdatedLabel = new System.Windows.Forms.Label();

            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.kpiCardsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();

            // Card 1: Capacity & Registered
            this.capacityCardPanel = new System.Windows.Forms.Panel();
            this.capacityTitleLabel = new System.Windows.Forms.Label();
            this.capacityValueLabel = new System.Windows.Forms.Label();
            this.capacitySubLabel = new System.Windows.Forms.Label();
            this.capacityProgressBar = new System.Windows.Forms.ProgressBar();

            // Card 2: Currently Inside
            this.insideCardPanel = new System.Windows.Forms.Panel();
            this.insideTitleLabel = new System.Windows.Forms.Label();
            this.insideValueLabel = new System.Windows.Forms.Label();
            this.insideSubLabel = new System.Windows.Forms.Label();

            // Card 3: Checked In Turnout
            this.checkedInCardPanel = new System.Windows.Forms.Panel();
            this.checkedInTitleLabel = new System.Windows.Forms.Label();
            this.checkedInValueLabel = new System.Windows.Forms.Label();
            this.checkedInSubLabel = new System.Windows.Forms.Label();

            // Card 4: Average Rating
            this.ratingCardPanel = new System.Windows.Forms.Panel();
            this.ratingTitleLabel = new System.Windows.Forms.Label();
            this.ratingValueLabel = new System.Windows.Forms.Label();
            this.ratingSubLabel = new System.Windows.Forms.Label();

            // Upper Charts Row
            this.upperChartsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.timelineGroupBox = new System.Windows.Forms.GroupBox();
            this.timelineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timelineNavPanel = new System.Windows.Forms.Panel();
            this.prevNavButton = new System.Windows.Forms.Button();
            this.timeFilterComboBox = new System.Windows.Forms.ComboBox();
            this.nextNavButton = new System.Windows.Forms.Button();
            this.resetFilterButton = new System.Windows.Forms.Button();

            this.doughnutGroupBox = new System.Windows.Forms.GroupBox();
            this.attendanceDoughnutChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // Lower Row
            this.lowerLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ratingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ratingsBarChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.logsGroupBox = new System.Windows.Forms.GroupBox();
            this.logsHeaderLabel = new System.Windows.Forms.Label();
            this.logsListBox = new System.Windows.Forms.ListBox();

            this.dashboardTimer = new System.Windows.Forms.Timer(this.components);

            this.contentPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.mainLayoutPanel.SuspendLayout();
            this.kpiCardsLayoutPanel.SuspendLayout();
            this.capacityCardPanel.SuspendLayout();
            this.insideCardPanel.SuspendLayout();
            this.checkedInCardPanel.SuspendLayout();
            this.ratingCardPanel.SuspendLayout();
            this.upperChartsLayoutPanel.SuspendLayout();
            this.timelineGroupBox.SuspendLayout();
            this.timelineNavPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timelineChart)).BeginInit();
            this.doughnutGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDoughnutChart)).BeginInit();
            this.lowerLayoutPanel.SuspendLayout();
            this.ratingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ratingsBarChart)).BeginInit();
            this.logsGroupBox.SuspendLayout();
            this.SuspendLayout();

            // contentPanel
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.contentPanel.Controls.Add(this.mainLayoutPanel);
            this.contentPanel.Controls.Add(this.topPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.contentPanel.Size = new System.Drawing.Size(1184, 780);
            this.contentPanel.TabIndex = 1;

            // topPanel
            this.topPanel.BackColor = System.Drawing.Color.Transparent;
            this.topPanel.Controls.Add(this.eventLabel);
            this.topPanel.Controls.Add(this.eventComboBox);
            this.topPanel.Controls.Add(this.lastUpdatedLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(12, 8);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1160, 36);
            this.topPanel.TabIndex = 0;

            // eventLabel
            this.eventLabel.AutoSize = true;
            this.eventLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.eventLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.eventLabel.Location = new System.Drawing.Point(2, 8);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(49, 19);
            this.eventLabel.TabIndex = 0;
            this.eventLabel.Text = "Event:";

            // eventComboBox
            this.eventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventComboBox.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.Location = new System.Drawing.Point(58, 5);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(450, 25);
            this.eventComboBox.TabIndex = 1;
            this.eventComboBox.SelectedIndexChanged += new System.EventHandler(this.EventComboBox_SelectedIndexChanged);

            // lastUpdatedLabel
            this.lastUpdatedLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lastUpdatedLabel.AutoSize = true;
            this.lastUpdatedLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lastUpdatedLabel.ForeColor = System.Drawing.Color.FromArgb(95, 95, 95);
            this.lastUpdatedLabel.Location = new System.Drawing.Point(970, 10);
            this.lastUpdatedLabel.Name = "lastUpdatedLabel";
            this.lastUpdatedLabel.Size = new System.Drawing.Size(107, 15);
            this.lastUpdatedLabel.TabIndex = 2;
            this.lastUpdatedLabel.Text = "Last Updated: --:--";

            // mainLayoutPanel
            this.mainLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Controls.Add(this.kpiCardsLayoutPanel, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.upperChartsLayoutPanel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.lowerLayoutPanel, 0, 2);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(12, 44);
            this.mainLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 3;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(1160, 660);
            this.mainLayoutPanel.TabIndex = 1;

            // kpiCardsLayoutPanel
            this.kpiCardsLayoutPanel.ColumnCount = 4;
            this.kpiCardsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiCardsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiCardsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiCardsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiCardsLayoutPanel.Controls.Add(this.capacityCardPanel, 0, 0);
            this.kpiCardsLayoutPanel.Controls.Add(this.insideCardPanel, 1, 0);
            this.kpiCardsLayoutPanel.Controls.Add(this.checkedInCardPanel, 2, 0);
            this.kpiCardsLayoutPanel.Controls.Add(this.ratingCardPanel, 3, 0);
            this.kpiCardsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiCardsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.kpiCardsLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.kpiCardsLayoutPanel.Name = "kpiCardsLayoutPanel";
            this.kpiCardsLayoutPanel.RowCount = 1;
            this.kpiCardsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.kpiCardsLayoutPanel.Size = new System.Drawing.Size(1160, 82);
            this.kpiCardsLayoutPanel.TabIndex = 0;

            // capacityCardPanel
            this.capacityCardPanel.BackColor = System.Drawing.Color.White;
            this.capacityCardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capacityCardPanel.Controls.Add(this.capacityProgressBar);
            this.capacityCardPanel.Controls.Add(this.capacitySubLabel);
            this.capacityCardPanel.Controls.Add(this.capacityValueLabel);
            this.capacityCardPanel.Controls.Add(this.capacityTitleLabel);
            this.capacityCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capacityCardPanel.Location = new System.Drawing.Point(2, 2);
            this.capacityCardPanel.Margin = new System.Windows.Forms.Padding(2);
            this.capacityCardPanel.Name = "capacityCardPanel";
            this.capacityCardPanel.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.capacityCardPanel.Size = new System.Drawing.Size(286, 78);
            this.capacityCardPanel.TabIndex = 0;

            // capacityTitleLabel
            this.capacityTitleLabel.AutoSize = true;
            this.capacityTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.capacityTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.capacityTitleLabel.Location = new System.Drawing.Point(8, 6);
            this.capacityTitleLabel.Name = "capacityTitleLabel";
            this.capacityTitleLabel.Size = new System.Drawing.Size(139, 13);
            this.capacityTitleLabel.TabIndex = 0;
            this.capacityTitleLabel.Text = "CAPACITY & REGISTERED";

            // capacityValueLabel
            this.capacityValueLabel.AutoSize = true;
            this.capacityValueLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.capacityValueLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.capacityValueLabel.Location = new System.Drawing.Point(7, 21);
            this.capacityValueLabel.Name = "capacityValueLabel";
            this.capacityValueLabel.Size = new System.Drawing.Size(65, 28);
            this.capacityValueLabel.TabIndex = 1;
            this.capacityValueLabel.Text = "0 / 0";

            // capacityProgressBar
            this.capacityProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.capacityProgressBar.Location = new System.Drawing.Point(10, 52);
            this.capacityProgressBar.Name = "capacityProgressBar";
            this.capacityProgressBar.Size = new System.Drawing.Size(160, 12);
            this.capacityProgressBar.TabIndex = 2;

            // capacitySubLabel
            this.capacitySubLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.capacitySubLabel.AutoSize = true;
            this.capacitySubLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.capacitySubLabel.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.capacitySubLabel.Location = new System.Drawing.Point(176, 52);
            this.capacitySubLabel.Name = "capacitySubLabel";
            this.capacitySubLabel.Size = new System.Drawing.Size(56, 13);
            this.capacitySubLabel.TabIndex = 3;
            this.capacitySubLabel.Text = "0% booked";

            // insideCardPanel
            this.insideCardPanel.BackColor = System.Drawing.Color.White;
            this.insideCardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.insideCardPanel.Controls.Add(this.insideSubLabel);
            this.insideCardPanel.Controls.Add(this.insideValueLabel);
            this.insideCardPanel.Controls.Add(this.insideTitleLabel);
            this.insideCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insideCardPanel.Location = new System.Drawing.Point(292, 2);
            this.insideCardPanel.Margin = new System.Windows.Forms.Padding(2);
            this.insideCardPanel.Name = "insideCardPanel";
            this.insideCardPanel.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.insideCardPanel.Size = new System.Drawing.Size(286, 78);
            this.insideCardPanel.TabIndex = 1;

            // insideTitleLabel
            this.insideTitleLabel.AutoSize = true;
            this.insideTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.insideTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.insideTitleLabel.Location = new System.Drawing.Point(8, 6);
            this.insideTitleLabel.Name = "insideTitleLabel";
            this.insideTitleLabel.Size = new System.Drawing.Size(110, 13);
            this.insideTitleLabel.TabIndex = 0;
            this.insideTitleLabel.Text = "CURRENTLY INSIDE";

            // insideValueLabel
            this.insideValueLabel.AutoSize = true;
            this.insideValueLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.insideValueLabel.ForeColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this.insideValueLabel.Location = new System.Drawing.Point(7, 21);
            this.insideValueLabel.Name = "insideValueLabel";
            this.insideValueLabel.Size = new System.Drawing.Size(26, 30);
            this.insideValueLabel.TabIndex = 1;
            this.insideValueLabel.Text = "0";

            // insideSubLabel
            this.insideSubLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.insideSubLabel.AutoSize = true;
            this.insideSubLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.insideSubLabel.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.insideSubLabel.Location = new System.Drawing.Point(8, 52);
            this.insideSubLabel.Name = "insideSubLabel";
            this.insideSubLabel.Size = new System.Drawing.Size(89, 15);
            this.insideSubLabel.TabIndex = 2;
            this.insideSubLabel.Text = "Checked out: 0";

            // checkedInCardPanel
            this.checkedInCardPanel.BackColor = System.Drawing.Color.White;
            this.checkedInCardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedInCardPanel.Controls.Add(this.checkedInSubLabel);
            this.checkedInCardPanel.Controls.Add(this.checkedInValueLabel);
            this.checkedInCardPanel.Controls.Add(this.checkedInTitleLabel);
            this.checkedInCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedInCardPanel.Location = new System.Drawing.Point(582, 2);
            this.checkedInCardPanel.Margin = new System.Windows.Forms.Padding(2);
            this.checkedInCardPanel.Name = "checkedInCardPanel";
            this.checkedInCardPanel.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.checkedInCardPanel.Size = new System.Drawing.Size(286, 78);
            this.checkedInCardPanel.TabIndex = 2;

            // checkedInTitleLabel
            this.checkedInTitleLabel.AutoSize = true;
            this.checkedInTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.checkedInTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.checkedInTitleLabel.Location = new System.Drawing.Point(8, 6);
            this.checkedInTitleLabel.Name = "checkedInTitleLabel";
            this.checkedInTitleLabel.Size = new System.Drawing.Size(126, 13);
            this.checkedInTitleLabel.TabIndex = 0;
            this.checkedInTitleLabel.Text = "TOTAL CHECKED IN";

            // checkedInValueLabel
            this.checkedInValueLabel.AutoSize = true;
            this.checkedInValueLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.checkedInValueLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.checkedInValueLabel.Location = new System.Drawing.Point(7, 21);
            this.checkedInValueLabel.Name = "checkedInValueLabel";
            this.checkedInValueLabel.Size = new System.Drawing.Size(26, 30);
            this.checkedInValueLabel.TabIndex = 1;
            this.checkedInValueLabel.Text = "0";

            // checkedInSubLabel
            this.checkedInSubLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.checkedInSubLabel.AutoSize = true;
            this.checkedInSubLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.checkedInSubLabel.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.checkedInSubLabel.Location = new System.Drawing.Point(8, 52);
            this.checkedInSubLabel.Name = "checkedInSubLabel";
            this.checkedInSubLabel.Size = new System.Drawing.Size(176, 15);
            this.checkedInSubLabel.TabIndex = 2;
            this.checkedInSubLabel.Text = "0% turnout (0 not arrived)";

            // ratingCardPanel
            this.ratingCardPanel.BackColor = System.Drawing.Color.White;
            this.ratingCardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ratingCardPanel.Controls.Add(this.ratingSubLabel);
            this.ratingCardPanel.Controls.Add(this.ratingValueLabel);
            this.ratingCardPanel.Controls.Add(this.ratingTitleLabel);
            this.ratingCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ratingCardPanel.Location = new System.Drawing.Point(872, 2);
            this.ratingCardPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ratingCardPanel.Name = "ratingCardPanel";
            this.ratingCardPanel.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.ratingCardPanel.Size = new System.Drawing.Size(286, 78);
            this.ratingCardPanel.TabIndex = 3;

            // ratingTitleLabel
            this.ratingTitleLabel.AutoSize = true;
            this.ratingTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.ratingTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.ratingTitleLabel.Location = new System.Drawing.Point(8, 6);
            this.ratingTitleLabel.Name = "ratingTitleLabel";
            this.ratingTitleLabel.Size = new System.Drawing.Size(100, 13);
            this.ratingTitleLabel.TabIndex = 0;
            this.ratingTitleLabel.Text = "AVERAGE RATING";

            // ratingValueLabel
            this.ratingValueLabel.AutoSize = true;
            this.ratingValueLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.ratingValueLabel.ForeColor = System.Drawing.Color.FromArgb(218, 138, 20);
            this.ratingValueLabel.Location = new System.Drawing.Point(7, 21);
            this.ratingValueLabel.Name = "ratingValueLabel";
            this.ratingValueLabel.Size = new System.Drawing.Size(73, 30);
            this.ratingValueLabel.TabIndex = 1;
            this.ratingValueLabel.Text = "- / 5.0";

            // ratingSubLabel
            this.ratingSubLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.ratingSubLabel.AutoSize = true;
            this.ratingSubLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.ratingSubLabel.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.ratingSubLabel.Location = new System.Drawing.Point(8, 52);
            this.ratingSubLabel.Name = "ratingSubLabel";
            this.ratingSubLabel.Size = new System.Drawing.Size(155, 15);
            this.ratingSubLabel.TabIndex = 2;
            this.ratingSubLabel.Text = "0 reviews (0% response)";

            // upperChartsLayoutPanel
            this.upperChartsLayoutPanel.ColumnCount = 2;
            this.upperChartsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.upperChartsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.upperChartsLayoutPanel.Controls.Add(this.timelineGroupBox, 0, 0);
            this.upperChartsLayoutPanel.Controls.Add(this.doughnutGroupBox, 1, 0);
            this.upperChartsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upperChartsLayoutPanel.Location = new System.Drawing.Point(0, 90);
            this.upperChartsLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.upperChartsLayoutPanel.Name = "upperChartsLayoutPanel";
            this.upperChartsLayoutPanel.RowCount = 1;
            this.upperChartsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.upperChartsLayoutPanel.Size = new System.Drawing.Size(1160, 279);
            this.upperChartsLayoutPanel.TabIndex = 1;

            // timelineGroupBox (Vertical Bar Chart with navigation on bottom)
            this.timelineGroupBox.BackColor = System.Drawing.Color.White;
            this.timelineGroupBox.Controls.Add(this.timelineChart);
            this.timelineGroupBox.Controls.Add(this.timelineNavPanel);
            this.timelineGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timelineGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.timelineGroupBox.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.timelineGroupBox.Location = new System.Drawing.Point(2, 2);
            this.timelineGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.timelineGroupBox.Name = "timelineGroupBox";
            this.timelineGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.timelineGroupBox.Size = new System.Drawing.Size(634, 275);
            this.timelineGroupBox.TabIndex = 0;
            this.timelineGroupBox.TabStop = false;
            this.timelineGroupBox.Text = "Check-Ins Volume (Vertical Bar Chart)";

            // timelineChart
            columnChartArea.AxisX.Interval = 1D;
            columnChartArea.AxisX.IsLabelAutoFit = true;
            columnChartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            columnChartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            columnChartArea.AxisX.LineColor = System.Drawing.Color.FromArgb(215, 215, 215);
            columnChartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(245, 245, 245);
            columnChartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            columnChartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            columnChartArea.AxisY.LineColor = System.Drawing.Color.FromArgb(215, 215, 215);
            columnChartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(240, 240, 240);
            columnChartArea.BackColor = System.Drawing.Color.White;
            columnChartArea.Name = "ColumnArea";
            this.timelineChart.ChartAreas.Add(columnChartArea);
            this.timelineChart.Dock = System.Windows.Forms.DockStyle.Fill;
            columnLegend.Alignment = System.Drawing.StringAlignment.Far;
            columnLegend.BackColor = System.Drawing.Color.Transparent;
            columnLegend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            columnLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            columnLegend.Name = "ColumnLegend";
            this.timelineChart.Legends.Add(columnLegend);
            this.timelineChart.Location = new System.Drawing.Point(6, 22);
            this.timelineChart.Margin = new System.Windows.Forms.Padding(2);
            this.timelineChart.Name = "timelineChart";
            columnSeries.ChartArea = "ColumnArea";
            columnSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            columnSeries.Color = System.Drawing.Color.FromArgb(43, 87, 154);
            columnSeries.IsValueShownAsLabel = true;
            columnSeries.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            columnSeries.Legend = "ColumnLegend";
            columnSeries.Name = "Check-ins";
            this.timelineChart.Series.Add(columnSeries);
            this.timelineChart.Size = new System.Drawing.Size(622, 215);
            this.timelineChart.TabIndex = 0;

            // timelineNavPanel (Navigation control on the bottom)
            this.timelineNavPanel.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.timelineNavPanel.Controls.Add(this.resetFilterButton);
            this.timelineNavPanel.Controls.Add(this.nextNavButton);
            this.timelineNavPanel.Controls.Add(this.timeFilterComboBox);
            this.timelineNavPanel.Controls.Add(this.prevNavButton);
            this.timelineNavPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timelineNavPanel.Location = new System.Drawing.Point(6, 237);
            this.timelineNavPanel.Name = "timelineNavPanel";
            this.timelineNavPanel.Padding = new System.Windows.Forms.Padding(2);
            this.timelineNavPanel.Size = new System.Drawing.Size(622, 32);
            this.timelineNavPanel.TabIndex = 1;

            // prevNavButton
            this.prevNavButton.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.prevNavButton.ForeColor = System.Drawing.Color.Black;
            this.prevNavButton.Location = new System.Drawing.Point(4, 3);
            this.prevNavButton.Name = "prevNavButton";
            this.prevNavButton.Size = new System.Drawing.Size(65, 26);
            this.prevNavButton.TabIndex = 0;
            this.prevNavButton.Text = "< Prev";
            this.prevNavButton.UseVisualStyleBackColor = true;
            this.prevNavButton.Click += new System.EventHandler(this.PrevNavButton_Click);

            // timeFilterComboBox
            this.timeFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.timeFilterComboBox.FormattingEnabled = true;
            this.timeFilterComboBox.Items.AddRange(new object[] {
                "All Times (Full Event)",
                "Morning (Before 12:00 PM)",
                "Afternoon (12:00 PM - 5:00 PM)",
                "Evening (After 5:00 PM)"
            });
            this.timeFilterComboBox.Location = new System.Drawing.Point(75, 5);
            this.timeFilterComboBox.Name = "timeFilterComboBox";
            this.timeFilterComboBox.Size = new System.Drawing.Size(220, 21);
            this.timeFilterComboBox.TabIndex = 1;
            this.timeFilterComboBox.SelectedIndex = 0;
            this.timeFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeFilterComboBox_SelectedIndexChanged);

            // nextNavButton
            this.nextNavButton.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.nextNavButton.ForeColor = System.Drawing.Color.Black;
            this.nextNavButton.Location = new System.Drawing.Point(301, 3);
            this.nextNavButton.Name = "nextNavButton";
            this.nextNavButton.Size = new System.Drawing.Size(65, 26);
            this.nextNavButton.TabIndex = 2;
            this.nextNavButton.Text = "Next >";
            this.nextNavButton.UseVisualStyleBackColor = true;
            this.nextNavButton.Click += new System.EventHandler(this.NextNavButton_Click);

            // resetFilterButton
            this.resetFilterButton.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.resetFilterButton.ForeColor = System.Drawing.Color.Black;
            this.resetFilterButton.Location = new System.Drawing.Point(372, 3);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(75, 26);
            this.resetFilterButton.TabIndex = 3;
            this.resetFilterButton.Text = "Reset";
            this.resetFilterButton.UseVisualStyleBackColor = true;
            this.resetFilterButton.Click += new System.EventHandler(this.ResetFilterButton_Click);

            // doughnutGroupBox (Doughnut Chart)
            this.doughnutGroupBox.BackColor = System.Drawing.Color.White;
            this.doughnutGroupBox.Controls.Add(this.attendanceDoughnutChart);
            this.doughnutGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doughnutGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.doughnutGroupBox.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.doughnutGroupBox.Location = new System.Drawing.Point(640, 2);
            this.doughnutGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.doughnutGroupBox.Name = "doughnutGroupBox";
            this.doughnutGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.doughnutGroupBox.Size = new System.Drawing.Size(518, 275);
            this.doughnutGroupBox.TabIndex = 1;
            this.doughnutGroupBox.TabStop = false;
            this.doughnutGroupBox.Text = "Attendance Status (Doughnut Chart)";

            // attendanceDoughnutChart
            doughnutChartArea.BackColor = System.Drawing.Color.White;
            doughnutChartArea.Name = "DoughnutArea";
            this.attendanceDoughnutChart.ChartAreas.Add(doughnutChartArea);
            this.attendanceDoughnutChart.Dock = System.Windows.Forms.DockStyle.Fill;
            doughnutLegend.BackColor = System.Drawing.Color.Transparent;
            doughnutLegend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            doughnutLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            doughnutLegend.Name = "DoughnutLegend";
            this.attendanceDoughnutChart.Legends.Add(doughnutLegend);
            this.attendanceDoughnutChart.Location = new System.Drawing.Point(6, 22);
            this.attendanceDoughnutChart.Name = "attendanceDoughnutChart";
            doughnutSeries.ChartArea = "DoughnutArea";
            doughnutSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            doughnutSeries.CustomProperties = "DoughnutRadius=58, PieLabelStyle=Disabled";
            doughnutSeries.Legend = "DoughnutLegend";
            doughnutSeries.Name = "AttendanceBreakdown";
            this.attendanceDoughnutChart.Series.Add(doughnutSeries);
            this.attendanceDoughnutChart.Size = new System.Drawing.Size(506, 247);
            this.attendanceDoughnutChart.TabIndex = 0;

            // lowerLayoutPanel
            this.lowerLayoutPanel.ColumnCount = 2;
            this.lowerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.lowerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.lowerLayoutPanel.Controls.Add(this.ratingsGroupBox, 0, 0);
            this.lowerLayoutPanel.Controls.Add(this.logsGroupBox, 1, 0);
            this.lowerLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lowerLayoutPanel.Location = new System.Drawing.Point(0, 375);
            this.lowerLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.lowerLayoutPanel.Name = "lowerLayoutPanel";
            this.lowerLayoutPanel.RowCount = 1;
            this.lowerLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lowerLayoutPanel.Size = new System.Drawing.Size(1160, 285);
            this.lowerLayoutPanel.TabIndex = 2;

            // ratingsGroupBox (Horizontal Bar Chart)
            this.ratingsGroupBox.BackColor = System.Drawing.Color.White;
            this.ratingsGroupBox.Controls.Add(this.ratingsBarChart);
            this.ratingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ratingsGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ratingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.ratingsGroupBox.Location = new System.Drawing.Point(2, 2);
            this.ratingsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.ratingsGroupBox.Name = "ratingsGroupBox";
            this.ratingsGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.ratingsGroupBox.Size = new System.Drawing.Size(483, 281);
            this.ratingsGroupBox.TabIndex = 0;
            this.ratingsGroupBox.TabStop = false;
            this.ratingsGroupBox.Text = "Review Ratings (Horizontal Bar Chart)";

            // ratingsBarChart
            barChartArea.AxisX.Interval = 1D;
            barChartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            barChartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            barChartArea.AxisX.LineColor = System.Drawing.Color.FromArgb(215, 215, 215);
            barChartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(245, 245, 245);
            barChartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            barChartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            barChartArea.AxisY.LineColor = System.Drawing.Color.FromArgb(215, 215, 215);
            barChartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(240, 240, 240);
            barChartArea.BackColor = System.Drawing.Color.White;
            barChartArea.Name = "BarArea";
            this.ratingsBarChart.ChartAreas.Add(barChartArea);
            this.ratingsBarChart.Dock = System.Windows.Forms.DockStyle.Fill;
            barLegend.Alignment = System.Drawing.StringAlignment.Far;
            barLegend.BackColor = System.Drawing.Color.Transparent;
            barLegend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            barLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            barLegend.Name = "BarLegend";
            this.ratingsBarChart.Legends.Add(barLegend);
            this.ratingsBarChart.Location = new System.Drawing.Point(6, 22);
            this.ratingsBarChart.Name = "ratingsBarChart";
            barSeries.ChartArea = "BarArea";
            barSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            barSeries.Color = System.Drawing.Color.FromArgb(91, 142, 219);
            barSeries.IsValueShownAsLabel = true;
            barSeries.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            barSeries.Legend = "BarLegend";
            barSeries.Name = "Ratings";
            this.ratingsBarChart.Series.Add(barSeries);
            this.ratingsBarChart.Size = new System.Drawing.Size(471, 253);
            this.ratingsBarChart.TabIndex = 0;

            // logsGroupBox (Comprehensive Event & System Logs)
            this.logsGroupBox.BackColor = System.Drawing.Color.White;
            this.logsGroupBox.Controls.Add(this.logsListBox);
            this.logsGroupBox.Controls.Add(this.logsHeaderLabel);
            this.logsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.logsGroupBox.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.logsGroupBox.Location = new System.Drawing.Point(489, 2);
            this.logsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.logsGroupBox.Name = "logsGroupBox";
            this.logsGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.logsGroupBox.Size = new System.Drawing.Size(669, 281);
            this.logsGroupBox.TabIndex = 1;
            this.logsGroupBox.TabStop = false;
            this.logsGroupBox.Text = "Live System & Activity Logs";

            // logsHeaderLabel
            this.logsHeaderLabel.BackColor = System.Drawing.Color.FromArgb(240, 244, 250);
            this.logsHeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logsHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.logsHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(43, 87, 154);
            this.logsHeaderLabel.Location = new System.Drawing.Point(6, 22);
            this.logsHeaderLabel.Name = "logsHeaderLabel";
            this.logsHeaderLabel.Padding = new System.Windows.Forms.Padding(4);
            this.logsHeaderLabel.Size = new System.Drawing.Size(657, 26);
            this.logsHeaderLabel.TabIndex = 0;
            this.logsHeaderLabel.Text = "Status: Capacity: 0 | Inside: 0 | Checked in: 0 | Average Rating: - / 5.0";
            this.logsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // logsListBox
            this.logsListBox.BackColor = System.Drawing.Color.White;
            this.logsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsListBox.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.logsListBox.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.logsListBox.FormattingEnabled = true;
            this.logsListBox.IntegralHeight = false;
            this.logsListBox.ItemHeight = 13;
            this.logsListBox.Location = new System.Drawing.Point(6, 48);
            this.logsListBox.Name = "logsListBox";
            this.logsListBox.Size = new System.Drawing.Size(657, 227);
            this.logsListBox.TabIndex = 1;

            // dashboardTimer
            this.dashboardTimer.Enabled = true;
            this.dashboardTimer.Interval = 5000;
            this.dashboardTimer.Tick += new System.EventHandler(this.DashboardTimer_Tick);

            // DashboardForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 780);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1024, 700);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Live Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            this.contentPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainLayoutPanel.ResumeLayout(false);
            this.kpiCardsLayoutPanel.ResumeLayout(false);
            this.capacityCardPanel.ResumeLayout(false);
            this.capacityCardPanel.PerformLayout();
            this.insideCardPanel.ResumeLayout(false);
            this.insideCardPanel.PerformLayout();
            this.checkedInCardPanel.ResumeLayout(false);
            this.checkedInCardPanel.PerformLayout();
            this.ratingCardPanel.ResumeLayout(false);
            this.ratingCardPanel.PerformLayout();
            this.upperChartsLayoutPanel.ResumeLayout(false);
            this.timelineGroupBox.ResumeLayout(false);
            this.timelineNavPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timelineChart)).EndInit();
            this.doughnutGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDoughnutChart)).EndInit();
            this.lowerLayoutPanel.ResumeLayout(false);
            this.ratingsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ratingsBarChart)).EndInit();
            this.logsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.ComboBox eventComboBox;
        private System.Windows.Forms.Label lastUpdatedLabel;

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel kpiCardsLayoutPanel;

        private System.Windows.Forms.Panel capacityCardPanel;
        private System.Windows.Forms.Label capacityTitleLabel;
        private System.Windows.Forms.Label capacityValueLabel;
        private System.Windows.Forms.Label capacitySubLabel;
        private System.Windows.Forms.ProgressBar capacityProgressBar;

        private System.Windows.Forms.Panel insideCardPanel;
        private System.Windows.Forms.Label insideTitleLabel;
        private System.Windows.Forms.Label insideValueLabel;
        private System.Windows.Forms.Label insideSubLabel;

        private System.Windows.Forms.Panel checkedInCardPanel;
        private System.Windows.Forms.Label checkedInTitleLabel;
        private System.Windows.Forms.Label checkedInValueLabel;
        private System.Windows.Forms.Label checkedInSubLabel;

        private System.Windows.Forms.Panel ratingCardPanel;
        private System.Windows.Forms.Label ratingTitleLabel;
        private System.Windows.Forms.Label ratingValueLabel;
        private System.Windows.Forms.Label ratingSubLabel;

        private System.Windows.Forms.TableLayoutPanel upperChartsLayoutPanel;
        private System.Windows.Forms.GroupBox timelineGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart timelineChart;
        private System.Windows.Forms.Panel timelineNavPanel;
        private System.Windows.Forms.Button prevNavButton;
        private System.Windows.Forms.ComboBox timeFilterComboBox;
        private System.Windows.Forms.Button nextNavButton;
        private System.Windows.Forms.Button resetFilterButton;

        private System.Windows.Forms.GroupBox doughnutGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart attendanceDoughnutChart;

        private System.Windows.Forms.TableLayoutPanel lowerLayoutPanel;
        private System.Windows.Forms.GroupBox ratingsGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart ratingsBarChart;

        private System.Windows.Forms.GroupBox logsGroupBox;
        private System.Windows.Forms.Label logsHeaderLabel;
        private System.Windows.Forms.ListBox logsListBox;

        private System.Windows.Forms.Timer dashboardTimer;
    }
}
