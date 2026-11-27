using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            LoadEventsIntoDropdown();
            dashboardTimer.Interval = 5000;
            dashboardTimer.Start();
        }

        private void LoadEventsIntoDropdown()
        {
            DataTable eventsTable = EventService.GetEvent();
            eventComboBox.DisplayMember = "name";
            eventComboBox.ValueMember = "event_id";
            eventComboBox.DataSource = eventsTable;

            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                RefreshDashboard(eventId);
            }
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                RefreshDashboard(eventId);
            }
        }

        private void DashboardTimer_Tick(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                RefreshDashboard(eventId);
            }
        }

        private void RefreshDashboard(int eventId)
        {
            DataRow? stats = DashboardService.GetDashboardStats(eventId);
            if (stats == null)
            {
                return;
            }

            int capacity = Convert.ToInt32(stats["capacity"]);
            int registered = Convert.ToInt32(stats["registered"]);
            int currentlyInside = Convert.ToInt32(stats["currently_inside"]);
            int checkedOut = Convert.ToInt32(stats["checked_out"]);
            int totalCheckedIn = Convert.ToInt32(stats["total_checked_in"]);
            int notArrived = Math.Max(0, registered - totalCheckedIn);
            int reviewCount = Convert.ToInt32(stats["review_count"]);
            double averageRating = Convert.ToDouble(stats["average_rating"]);

            UpdateKpiCards(eventId, capacity, registered, currentlyInside, checkedOut, totalCheckedIn, notArrived, reviewCount, averageRating);
            BindTimelineChart(eventId);
            BindAttendanceDoughnutChart(currentlyInside, checkedOut, notArrived);
            BindRatingsBarChart(eventId);
            BindComprehensiveLogs(eventId, capacity, currentlyInside, totalCheckedIn, averageRating, reviewCount);

            lastUpdatedLabel.Text = $"Last updated: {DateTime.Now:hh:mm:ss tt}";
        }

        private void UpdateKpiCards(int eventId, int capacity, int registered, int inside, int checkedOut, int totalCheckedIn, int notArrived, int reviewCount, double avgRating)
        {
            // Card 1: Capacity & Registered
            double regPercent = capacity > 0 ? (double)registered / capacity : 0.0;
            capacityValueLabel.Text = $"{registered} / {capacity}";
            capacityProgressBar.Maximum = Math.Max(1, capacity);
            capacityProgressBar.Value = Math.Clamp(registered, 0, capacityProgressBar.Maximum);
            capacitySubLabel.Text = $"{regPercent:P0} booked";

            // Card 2: Currently Inside
            insideValueLabel.Text = inside.ToString();
            insideSubLabel.Text = $"Checked out: {checkedOut}";

            // Card 3: Checked In Turnout
            double turnoutPercent = registered > 0 ? (double)totalCheckedIn / registered : 0.0;
            checkedInValueLabel.Text = totalCheckedIn.ToString();
            checkedInSubLabel.Text = $"{turnoutPercent:P0} turnout ({notArrived} not arrived)";

            // Card 4: Average Rating
            double responsePercent = totalCheckedIn > 0 ? (double)reviewCount / totalCheckedIn : 0.0;
            ratingValueLabel.Text = reviewCount > 0 ? $"{avgRating:F1} / 5.0" : "- / 5.0";
            ratingSubLabel.Text = $"{reviewCount} reviews ({responsePercent:P0} response)";

            // Card 5: Registration Status
            bool isRegOpen = EventService.IsRegistrationOpen(eventId);
            if (isRegOpen)
            {
                registrationValueLabel.Text = "OPEN";
                registrationValueLabel.ForeColor = Color.FromArgb(34, 139, 34);
                registrationSubLabel.Text = registered >= capacity && capacity > 0 ? "Event is full" : "Accepting attendees";
            }
            else
            {
                registrationValueLabel.Text = "CLOSED";
                registrationValueLabel.ForeColor = Color.FromArgb(192, 0, 0);
                registrationSubLabel.Text = "Registration closed";
            }
        }

        private void BindAttendanceDoughnutChart(int inside, int checkedOut, int notArrived)
        {
            var series = attendanceDoughnutChart.Series["AttendanceBreakdown"];
            series.Points.Clear();

            var pInside = new DataPoint();
            pInside.SetValueXY("Inside", inside);
            pInside.LegendText = $"Inside ({inside})";
            pInside.Color = Color.FromArgb(34, 139, 34);
            pInside.ToolTip = $"Inside: {inside}";
            series.Points.Add(pInside);

            var pOut = new DataPoint();
            pOut.SetValueXY("Checked Out", checkedOut);
            pOut.LegendText = $"Checked Out ({checkedOut})";
            pOut.Color = Color.FromArgb(91, 142, 219);
            pOut.ToolTip = $"Checked Out: {checkedOut}";
            series.Points.Add(pOut);

            var pNotArrived = new DataPoint();
            pNotArrived.SetValueXY("Not Arrived", notArrived);
            pNotArrived.LegendText = $"Not Arrived ({notArrived})";
            pNotArrived.Color = Color.FromArgb(220, 226, 235);
            pNotArrived.ToolTip = $"Not Arrived: {notArrived}";
            series.Points.Add(pNotArrived);
        }

        private void BindTimelineChart(int eventId)
        {
            var series = timelineChart.Series["Check-ins"];
            series.Points.Clear();

            DataTable timelineTable = DashboardService.GetCheckInTimeline(eventId);
            int filterIndex = timeFilterComboBox.SelectedIndex;

            var slotMap = new System.Collections.Generic.Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (DataRow row in timelineTable.Rows)
            {
                if (row["checked_in_at"] == DBNull.Value)
                {
                    continue;
                }

                DateTime checkTime = Convert.ToDateTime(row["checked_in_at"]);
                if (!MatchesTimeFilter(checkTime, filterIndex))
                {
                    continue;
                }

                string hourSlot = new DateTime(checkTime.Year, checkTime.Month, checkTime.Day, checkTime.Hour, 0, 0)
                    .ToString("h:00 tt", System.Globalization.CultureInfo.InvariantCulture);

                if (!slotMap.ContainsKey(hourSlot))
                {
                    slotMap[hourSlot] = 1;
                }
                else
                {
                    slotMap[hourSlot]++;
                }
            }

            var chartArea = timelineChart.ChartAreas["ColumnArea"];
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Interval = 1;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(70, 70, 70);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);

            if (slotMap.Count == 0)
            {
                var emptyPoint = new DataPoint();
                emptyPoint.SetValueXY(1, 0);
                emptyPoint.AxisLabel = "(No Check-ins)";
                emptyPoint.IsValueShownAsLabel = false;
                series.Points.Add(emptyPoint);

                chartArea.AxisX.Minimum = 0.5;
                chartArea.AxisX.Maximum = 1.5;
                chartArea.AxisY.Minimum = 0;
                chartArea.AxisY.Maximum = 5;
                chartArea.AxisY.Interval = 1;
                return;
            }

            int xIndex = 1;
            int maxCount = 0;

            foreach (var kvp in slotMap)
            {
                string slotName = kvp.Key;
                int count = kvp.Value;

                if (count > maxCount)
                {
                    maxCount = count;
                }

                var point = new DataPoint();
                point.SetValueXY(xIndex, count);
                point.AxisLabel = slotName;
                point.Label = $"{slotName}\n({count})";
                point.ToolTip = $"{slotName}: {count} check-ins";
                point.Color = Color.FromArgb(43, 87, 154);
                point.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                series.Points.Add(point);

                xIndex++;
            }

            chartArea.AxisX.Minimum = 0.5;
            chartArea.AxisX.Maximum = slotMap.Count + 0.5;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = Math.Max(maxCount + 2, 5);
            chartArea.AxisY.Interval = double.NaN;
        }

        private static bool MatchesTimeFilter(DateTime time, int filterIndex)
        {
            if (filterIndex == 1)
            {
                return time.Hour < 12; // Morning
            }
            if (filterIndex == 2)
            {
                return time.Hour >= 12 && time.Hour < 17; // Afternoon
            }
            if (filterIndex == 3)
            {
                return time.Hour >= 17; // Evening
            }

            return true; // All Times
        }

        private void BindRatingsBarChart(int eventId)
        {
            var series = ratingsBarChart.Series["Ratings"];
            series.Points.Clear();

            DataTable ratingsTable = DashboardService.GetRatingDistribution(eventId);
            int[] starCounts = new int[6]; // index 1 to 5

            foreach (DataRow row in ratingsTable.Rows)
            {
                int rating = Convert.ToInt32(row["rating"]);
                int count = Convert.ToInt32(row["total"]);
                if (rating >= 1 && rating <= 5)
                {
                    starCounts[rating] = count;
                }
            }

            int maxCount = 0;
            for (int star = 1; star <= 5; star++)
            {
                int count = starCounts[star];
                if (count > maxCount)
                {
                    maxCount = count;
                }

                var point = new DataPoint();
                point.SetValueXY(star, count);
                point.AxisLabel = $"{star}";
                point.Label = count > 0 ? $"{count}" : "";
                point.ToolTip = $"{star} Stars: {count} reviews";
                point.Color = Color.FromArgb(91, 142, 219);
                point.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                series.Points.Add(point);
            }

            var barArea = ratingsBarChart.ChartAreas["BarArea"];
            barArea.AxisX.Minimum = 0.5;
            barArea.AxisX.Maximum = 5.5;
            barArea.AxisX.Interval = 1;
            barArea.AxisX.LabelStyle.Interval = 1;
            barArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            barArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(70, 70, 70);
            barArea.AxisX.Title = "Stars (1 - 5)";
            barArea.AxisX.TitleFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            barArea.AxisX.TitleForeColor = Color.FromArgb(100, 100, 100);

            if (maxCount == 0)
            {
                barArea.AxisY.Minimum = 0;
                barArea.AxisY.Maximum = 5;
                barArea.AxisY.Interval = 1;
            }
            else
            {
                barArea.AxisY.Minimum = 0;
                barArea.AxisY.Maximum = Math.Max(maxCount + 2, 5);
                barArea.AxisY.Interval = double.NaN;
            }
        }

        private void BindComprehensiveLogs(int eventId, int capacity, int inside, int checkedIn, double avgRating, int reviewCount)
        {
            string ratingDisplay = reviewCount > 0 ? $"{avgRating:F1}" : "-";
            logsHeaderLabel.Text = $"Status: Capacity: {capacity} | Inside: {inside} | Checked in: {checkedIn} | Average Rating: {ratingDisplay} / 5.0";

            DataTable logsTable = DashboardService.GetComprehensiveLogs(eventId);

            logsListBox.BeginUpdate();
            logsListBox.Items.Clear();

            foreach (DataRow row in logsTable.Rows)
            {
                string logText = row["log_text"].ToString() ?? "";
                if (row["log_time"] != DBNull.Value)
                {
                    DateTime time = Convert.ToDateTime(row["log_time"]);
                    logsListBox.Items.Add($"[{time:MM/dd h:mm tt}] {logText}");
                }
                else
                {
                    logsListBox.Items.Add(logText);
                }
            }

            logsListBox.EndUpdate();
        }

        private void PrevNavButton_Click(object sender, EventArgs e)
        {
            if (timeFilterComboBox.SelectedIndex > 0)
            {
                timeFilterComboBox.SelectedIndex--;
            }
            else
            {
                timeFilterComboBox.SelectedIndex = timeFilterComboBox.Items.Count - 1;
            }
        }

        private void NextNavButton_Click(object sender, EventArgs e)
        {
            if (timeFilterComboBox.SelectedIndex < timeFilterComboBox.Items.Count - 1)
            {
                timeFilterComboBox.SelectedIndex++;
            }
            else
            {
                timeFilterComboBox.SelectedIndex = 0;
            }
        }

        private void ResetFilterButton_Click(object sender, EventArgs e)
        {
            timeFilterComboBox.SelectedIndex = 0;
        }

        private void TimeFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                BindTimelineChart(eventId);
            }
        }
    }
}
