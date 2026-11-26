using System;
using System.Data;
using System.Windows.Forms;
using Evently.Core.Services;

namespace Evently.Forms
{
    public partial class ReviewsForm : Form
    {
        public ReviewsForm()
        {
            InitializeComponent();
            reviewsDataGridView.AutoGenerateColumns = false;
            LoadEvents();
        }

        private void LoadEvents()
        {
            DataTable eventsTable = EventService.GetEvent();
            eventComboBox.DisplayMember = "name";
            eventComboBox.ValueMember = "event_id";
            eventComboBox.DataSource = eventsTable;

            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadReviews(eventId);
            }
        }

        private void LoadReviews(int eventId)
        {
            DataTable table = ReviewService.GetReviewsByEvent(eventId);
            reviewsDataGridView.DataSource = table;
            int count = table.Rows.Count;
            resultLabel.Text = count == 0 ? "No reviews yet for this event." : $"{count} review(s)";

            RefreshActivityLog();
            UpdateSelectedComment();
        }

        private void RefreshActivityLog()
        {
            string latest = ActivityLogService.GetLatest("reviews");

            if (latest == "")
            {
                latest = "No activity yet.";
            }

            activityLogLabel.Text = latest;
        }

        private void ReviewsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedComment();
        }

        private void UpdateSelectedComment()
        {
            if (reviewsDataGridView.CurrentRow != null && reviewsDataGridView.CurrentRow.Cells["commentColumn"] != null)
            {
                selectedCommentTextBox.Text = Convert.ToString(reviewsDataGridView.CurrentRow.Cells["commentColumn"].Value) ?? "";
            }
            else
            {
                selectedCommentTextBox.Text = "";
            }
        }

        private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadReviews(eventId);
            }
        }

    }
}