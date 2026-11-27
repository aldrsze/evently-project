using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.Tickets;

public class FindTicketModel : PageModel
{
    [BindProperty]
    public string TicketIdInput { get; set; } = string.Empty;

    public bool HasSearched { get; set; }
    public bool IsFound { get; set; }
    public bool IsApproved { get; set; }
    public string? ErrorMessage { get; set; }

    public string TicketId { get; set; } = string.Empty;
    public string AttendeeName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTime? EventDate { get; set; }
    public bool IsCheckedIn { get; set; }
    public string? QrCodeBase64 { get; set; }

    public void OnGet(string? ticketId)
    {
        if (!string.IsNullOrWhiteSpace(ticketId))
        {
            TicketIdInput = ticketId.Trim();
            LookupTicket(TicketIdInput);
        }
    }

    public IActionResult OnPost()
    {
        string id = TicketIdInput.Trim();
        if (string.IsNullOrWhiteSpace(id))
        {
            ErrorMessage = "Please enter your Ticket ID.";
            return Page();
        }

        LookupTicket(id);
        return Page();
    }

    private void LookupTicket(string id)
    {
        HasSearched = true;
        TicketId = id;

        DataRow? row = TicketService.GetTicketDetails(id);
        if (row == null)
        {
            IsFound = false;
            ErrorMessage = "No ticket found matching that ID. Please check your ticket code.";
            return;
        }

        IsFound = true;
        bool emailed = row["ticket_emailed"] != DBNull.Value && Convert.ToBoolean(row["ticket_emailed"]);
        IsApproved = emailed;

        AttendeeName = row["attendee_name"].ToString() ?? "";
        Email = row["email"].ToString() ?? "";
        EventName = row["event_name"].ToString() ?? "";
        Venue = row["venue"].ToString() ?? "";
        if (DateTime.TryParse(row["event_date"].ToString(), out DateTime dt))
        {
            EventDate = dt;
        }
        IsCheckedIn = row["checked_in_at"] != DBNull.Value;

        if (IsApproved)
        {
            byte[] qrBytes = QrCodeService.GenerateTicketQrPng(id);
            QrCodeBase64 = Convert.ToBase64String(qrBytes);
        }
    }
}
