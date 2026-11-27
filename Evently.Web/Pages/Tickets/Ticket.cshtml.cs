using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.Tickets;

public class TicketModel : PageModel
{
    public string TicketId { get; set; } = string.Empty;
    public string AttendeeName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTime? EventDate { get; set; }
    public bool IsCheckedIn { get; set; }
    public string? QrCodeBase64 { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnGet(string ticketId)
    {
        TicketId = ticketId;

        DataRow? row = TicketService.GetTicketDetails(ticketId);
        if (row == null)
        {
            ErrorMessage = "Ticket not found. Please verify your ticket link.";
            return;
        }

        AttendeeName = row["attendee_name"].ToString() ?? "";
        Email = row["email"].ToString() ?? "";
        EventName = row["event_name"].ToString() ?? "";
        Venue = row["venue"].ToString() ?? "";
        if (DateTime.TryParse(row["event_date"].ToString(), out DateTime date))
        {
            EventDate = date;
        }

        IsCheckedIn = row["checked_in_at"] != DBNull.Value;

        bool isEmailed = row["ticket_emailed"] != DBNull.Value && Convert.ToBoolean(row["ticket_emailed"]);
        if (!isEmailed)
        {
            ErrorMessage = "Your registration is currently pending organizer review. Your ticket will be emailed to you once approved.";
            return;
        }

        byte[] qrBytes = QrCodeService.GenerateTicketQrPng(ticketId);
        QrCodeBase64 = Convert.ToBase64String(qrBytes);
    }
}
