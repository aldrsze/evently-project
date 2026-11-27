using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.Registration;

public class RegisterModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int? EventId { get; set; }

    [BindProperty]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public DateTime? Birthdate { get; set; }

    public string? ErrorMessage { get; set; }

    public bool IsSubmitted { get; set; }

    public EventItem? SelectedEvent { get; set; }

    public List<EventItem> AvailableEvents { get; set; } = new();

    public void OnGet(int? eventId)
    {
        if (eventId.HasValue)
        {
            EventId = eventId.Value;
        }

        LoadEvents();
        if (EventId.HasValue)
        {
            SelectedEvent = AvailableEvents.Find(e => e.EventId == EventId.Value);

            if (SelectedEvent == null && !EventService.IsRegistrationOpen(EventId.Value))
            {
                ErrorMessage = "Registration for this event has closed.";
            }
        }
    }

    public IActionResult OnPost()
    {
        LoadEvents();
        if (EventId.HasValue)
        {
            SelectedEvent = AvailableEvents.Find(e => e.EventId == EventId.Value);
        }

        if (!EventId.HasValue || EventId.Value <= 0)
        {
            ErrorMessage = "Please select an event.";
            return Page();
        }

        if (!OrganizerService.EventHasManager(EventId.Value))
        {
            ErrorMessage = "This event is not open for registration yet. It has no manager assigned.";
            return Page();
        }

        if (!EventService.IsRegistrationOpen(EventId.Value))
        {
            ErrorMessage = "Registration for this event has closed.";
            return Page();
        }

        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email))
        {
            ErrorMessage = "First name, last name, and email are required.";
            return Page();
        }

        DateTime bDate = Birthdate ?? DateTime.Today.AddYears(-18);

        bool success = TicketService.AddAttendee(EventId.Value, FirstName.Trim(), LastName.Trim(), Email.Trim(), bDate);
        if (!success)
        {
            ErrorMessage = "Registration failed. The event may be at full capacity, or this email is already registered.";
            return Page();
        }

        IsSubmitted = true;
        return Page();
    }

    private void LoadEvents()
    {
        AvailableEvents.Clear();
        DataTable dt = EventService.GetEvent();
        foreach (DataRow row in dt.Rows)
        {
            int eventId = Convert.ToInt32(row["event_id"]);

            // an event with no manager or closed registration cannot take attendees
            if (!OrganizerService.EventHasManager(eventId))
            {
                continue;
            }

            if (!EventService.IsRegistrationOpen(eventId))
            {
                continue;
            }

            AvailableEvents.Add(new EventItem
            {
                EventId = eventId,
                Name = row["name"].ToString() ?? "",
                Venue = row["venue"].ToString() ?? ""
            });
        }
    }

    public class EventItem
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
    }
}
