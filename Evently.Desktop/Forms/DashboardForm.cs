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
            int ticketsEmailed = Convert.ToInt32(stats["tickets_emailed"]);
            int reviewRequestsSent = Convert.ToInt32(stats["review_requests_sent"]);
            int reviewCount = Convert.ToInt32(stats["review_count"]);
            double averageRating = Convert.ToDouble(stats["average_rating"]);

            // Text Metrics & Progress
            double registeredPercent = capacity > 0 ? (double)registered / capacity : 0;
            progressLabel.Text = $"Registered: {registered} of {capacity} ({registeredPercent:P0})";
            progressBar.Maximum = Math.Max(1, capacity);
            progressBar.Value = Math.Clamp(registered, 0, progressBar.Maximum);

            insideLabel.Text = $"Currently inside: {currentlyInside}";
            double turnoutPercent = registered > 0 ? (double)totalCheckedIn / registered : 0;
            checkedInLabel.Text = $"Checked in: {totalCheckedIn} ({turnoutPercent:P0} turnout)";
            notArrivedLabel.Text = $"Not yet arrived: {notArrived}";

            averageRatingLabel.Text = $"Average rating: {averageRating:F1} / 5.0 ({reviewCount} reviews)";
            double responsePercent = totalCheckedIn > 0 ? (double)reviewCount / totalCheckedIn : 0;
            responseRateLabel.Text = $"Response rate: {responsePercent:P0}";
            reviewRequestLabel.Text = $"Review requests sent: {reviewRequestsSent} of {totalCheckedIn}";

            ticketsEmailedLabel.Text = $"Tickets emailed: {ticketsEmailed} of {registered}";
            lastUpdatedLabel.Text = $"Last updated: {DateTime.Now:hh:mm:ss tt}";

            // Attendance Doughnut Chart
            BindAttendancePieChart(currentlyInside, checkedOut, notArrived);

            // Check-Ins Over Time Chart
            BindTimelineChart(eventId);

            // Recent Check-Ins Feed
            BindRecentActivity(eventId);
        }

        private void BindAttendancePieChart(int inside, int checkedOut, int notArrived)
        {
            var series = attendancePieChart.Series["AttendanceBreakdown"];
            series.Points.Clear();

            var pInside = new DataPoint();
            pInside.SetValueXY("Inside", inside);
            pInside.LegendText = $"Inside ({inside})";
            pInside.Color = Color.FromArgb(43, 87, 154);
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
            int cumulative = 0;
            string lastSlot = "";

            foreach (DataRow row in timelineTable.Rows)
            {
                cumulative++;
                DateTime checkTime = Convert.ToDateTime(row["checked_in_at"]);
                string slot = checkTime.ToString("h:mm tt");

                if (slot != lastSlot)
                {
                    var point = new DataPoint();
                    point.SetValueXY(slot, cumulative);
                    point.ToolTip = $"{slot}: {cumulative} total check-ins";
                    series.Points.Add(point);
                    lastSlot = slot;
                }
                else if (series.Points.Count > 0)
                {
                    series.Points[^1].SetValueXY(slot, cumulative);
                }
            }
        }

        private void BindRecentActivity(int eventId)
        {
            DataTable recentTable = DashboardService.GetRecentActivity(eventId);

            recentCheckInsListBox.BeginUpdate();
            recentCheckInsListBox.Items.Clear();

            foreach (DataRow row in recentTable.Rows)
            {
                string name = row["attendee_name"].ToString() ?? "";
                if (row["checked_out_at"] != DBNull.Value)
                {
                    DateTime outTime = Convert.ToDateTime(row["checked_out_at"]);
                    recentCheckInsListBox.Items.Add($"{name} - {outTime:h:mm tt} [Check-out]");
                }
                else if (row["checked_in_at"] != DBNull.Value)
                {
                    DateTime inTime = Convert.ToDateTime(row["checked_in_at"]);
                    recentCheckInsListBox.Items.Add($"{name} - {inTime:h:mm tt} [Check-in]");
                }
            }

            recentCheckInsListBox.EndUpdate();
        }
    }
}
