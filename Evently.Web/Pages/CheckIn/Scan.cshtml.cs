using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.CheckIn;

[Authorize]
public class ScanModel : PageModel
{
    [BindProperty]
    public string TicketId { get; set; } = string.Empty;

    public string? SuccessMessage { get; set; }
    public string? WarningMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    // form-post fallback for manual entry (desktop)
    public IActionResult OnPost(string action)
    {
        if (string.IsNullOrWhiteSpace(TicketId))
        {
            ErrorMessage = "Please enter or scan a ticket ID.";
            return Page();
        }

        string cleanId = TicketId.Trim();
        var result = ProcessScan(cleanId);

        if (result.Status == "success")
        {
            SuccessMessage = result.Message;
            TicketId = string.Empty;
        }
        else if (result.Status == "warning")
        {
            WarningMessage = result.Message;
            TicketId = string.Empty;
        }
        else
        {
            ErrorMessage = result.Message;
        }

        return Page();
    }

    // JSON endpoint for the camera scanner JS
    public IActionResult OnPostApi([FromBody] ScanRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.TicketId))
        {
            return new JsonResult(new { status = "error", message = "No ticket ID." });
        }

        string cleanId = request.TicketId.Trim();
        var result = ProcessScan(cleanId);
        return new JsonResult(result);
    }

    // auto-determines check-in vs check-out based on ticket state
    private ScanResult ProcessScan(string ticketId)
    {
        DataRow? ticket = TicketService.GetTicketDetails(ticketId);

        if (ticket == null)
        {
            return new ScanResult("error", "Invalid ticket.", "", "");
        }

        string name = ticket["attendee_name"].ToString() ?? "Attendee";
        string eventName = ticket["event_name"].ToString() ?? "Event";
        bool isApproved = Convert.ToBoolean(ticket["ticket_emailed"]);

        if (!isApproved)
        {
            return new ScanResult("error", $"Ticket for '{name}' is pending approval.", name, eventName);
        }

        bool checkedIn = ticket["checked_in_at"] != DBNull.Value;
        bool checkedOut = ticket["checked_out_at"] != DBNull.Value;

        // not checked in yet -> check in
        if (!checkedIn)
        {
            bool ok = TicketService.CheckInTicket(ticketId);
            if (ok)
            {
                return new ScanResult("success", $"Welcome, {name}!", name, eventName);
            }
            return new ScanResult("error", "Could not record check-in.", name, eventName);
        }

        // checked in but not out -> check out
        if (checkedIn && !checkedOut)
        {
            bool ok = TicketService.CheckOutTicket(ticketId);
            if (ok)
            {
                return new ScanResult("warning", $"Goodbye, {name}!", name, eventName);
            }
            return new ScanResult("error", "Could not record check-out.", name, eventName);
        }

        // already checked out
        return new ScanResult("error", $"Ticket already used — {name} has already left.", name, eventName);
    }

    public class ScanRequest
    {
        public string TicketId { get; set; } = string.Empty;
    }

    public class ScanResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string EventName { get; set; }

        public ScanResult(string status, string message, string name, string eventName)
        {
            Status = status;
            Message = message;
            Name = name;
            EventName = eventName;
        }
    }
}
