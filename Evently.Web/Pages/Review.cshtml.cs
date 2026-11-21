using System.Data;
using Evently.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evently.Web.Pages;

public class ReviewModel : PageModel
{
    public string? TicketId { get; set; }
    public string? AttendeeName { get; set; }
    public string? EventName { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSubmitted { get; set; }

    public void OnGet(string ticketId)
    {
        TicketId = ticketId;

        DataRow? row = ReviewService.GetTicketForReview(ticketId);

        if (row == null)
        {
            ErrorMessage = "Invalid review link. Please check your email and try again.";
            return;
        }

        if (row["checked_in_at"] == DBNull.Value)
        {
            ErrorMessage = "You have not checked in to this event. Reviews are only available to checked-in attendees.";
            return;
        }

        if (Convert.ToInt32(row["has_review"]) > 0)
        {
            IsSubmitted = true;
            return;
        }

        AttendeeName = row["attendee_name"].ToString();
        EventName = row["event_name"].ToString();
    }

    public IActionResult OnPost(string ticketId, int rating, string? comment)
    {
        if (rating < 1 || rating > 5)
        {
            ErrorMessage = "Please select a rating from 1 to 5.";
            TicketId = ticketId;

            DataRow? row = ReviewService.GetTicketForReview(ticketId);
            if (row != null)
            {
                AttendeeName = row["attendee_name"].ToString();
                EventName = row["event_name"].ToString();
            }
            return Page();
        }

        bool ok = ReviewService.SubmitReview(ticketId, rating, comment);

        if (!ok)
        {
            // Already submitted or ticket invalid — just show the thank-you state
        }

        IsSubmitted = true;
        return Page();
    }
}
