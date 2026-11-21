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
        }

        private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedValue != null && int.TryParse(eventComboBox.SelectedValue.ToString(), out int eventId))
            {
                LoadReviews(eventId);
            }
        }

        // These handlers exist because Designer.cs wires them; panels are hidden so buttons are never shown.
        private void saveUrlButton_Click(object sender, EventArgs e) { }
        private void sendRequestButton_Click(object sender, EventArgs e) { }
        private void importReviewsButton_Click(object sender, EventArgs e) { }
    }
}