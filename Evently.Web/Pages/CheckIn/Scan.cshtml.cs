using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.CheckIn;

public class ScanModel : PageModel
{
    [BindProperty]
    public string TicketId { get; set; } = string.Empty;

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost(string action)
    {
        if (string.IsNullOrWhiteSpace(TicketId))
        {
            ErrorMessage = "Please enter or scan a ticket ID.";
            return Page();
        }

        string cleanTicketId = TicketId.Trim();
        DataRow? ticket = TicketService.GetTicketDetails(cleanTicketId);

        if (ticket == null)
        {
            ErrorMessage = $"Invalid ticket: '{cleanTicketId}' not found.";
            return Page();
        }

        string attendeeName = ticket["attendee_name"].ToString() ?? "Attendee";
        string eventName = ticket["event_name"].ToString() ?? "Event";

        if (action == "checkin")
        {
            if (ticket["checked_in_at"] != DBNull.Value)
            {
                ErrorMessage = $"Already checked in: '{attendeeName}' was already checked in at {Convert.ToDateTime(ticket["checked_in_at"]):h:mm tt}.";
                return Page();
            }

            bool ok = TicketService.CheckInTicket(cleanTicketId);
            if (ok)
            {
                SuccessMessage = $"SUCCESS: '{attendeeName}' checked in to {eventName} at {DateTime.Now:h:mm tt}.";
                TicketId = string.Empty;
            }
            else
            {
                ErrorMessage = "Could not record check-in.";
            }
        }
        else if (action == "checkout")
        {
            if (ticket["checked_in_at"] == DBNull.Value)
            {
                ErrorMessage = $"Cannot check out: '{attendeeName}' has not checked in yet.";
                return Page();
            }

            if (ticket["checked_out_at"] != DBNull.Value)
            {
                ErrorMessage = $"Already checked out: '{attendeeName}' was already checked out at {Convert.ToDateTime(ticket["checked_out_at"]):h:mm tt}.";
                return Page();
            }

            bool ok = TicketService.CheckOutTicket(cleanTicketId);
            if (ok)
            {
                SuccessMessage = $"SUCCESS: '{attendeeName}' checked out from {eventName} at {DateTime.Now:h:mm tt}.";
                TicketId = string.Empty;
            }
            else
            {
                ErrorMessage = "Could not record check-out.";
            }
        }

        return Page();
    }
}
