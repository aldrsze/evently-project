using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages;

public class IndexModel : PageModel
{
    public List<EventCard> UpcomingEvents { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string CurrentFilter { get; set; } = "upcoming";

    public int CountAll { get; set; }
    public int CountUpcoming { get; set; }
    public int CountOpen { get; set; }
    public int CountFull { get; set; }
    public int CountEnded { get; set; }

    public void OnGet(string? filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            CurrentFilter = filter.Trim().ToLowerInvariant();
        }

        var allEvents = new List<EventCard>();
        DataTable table = EventService.GetEvent();
        DateTime now = DateTime.Now;

        foreach (DataRow row in table.Rows)
        {
            int eventId = Convert.ToInt32(row["event_id"]);

            string managerName = "";
            string managerEmail = "";
            DataTable managersTable = OrganizerService.GetOrganizersByEvent(eventId);
            if (managersTable.Rows.Count > 0)
            {
                DataRow mRow = managersTable.Rows[0];
                managerName = mRow["full_name"].ToString() ?? "";
                managerEmail = mRow["email"].ToString() ?? "";
            }

            DateTime eventDate = DateTime.TryParse(row["event_date"].ToString(), out DateTime dt) ? dt : now;
            int capacity = Convert.ToInt32(row["capacity"]);
            int remaining = EventService.GetRemainingCapacity(eventId);
            bool isRegOpen = EventService.IsRegistrationOpen(eventId);

            var card = new EventCard
            {
                EventId = eventId,
                Name = row["name"].ToString() ?? "",
                Venue = row["venue"].ToString() ?? "",
                Description = row["description"].ToString() ?? "",
                EventDate = eventDate,
                Capacity = capacity,
                RemainingCapacity = remaining,
                IsRegistrationOpen = isRegOpen,
                ManagerName = managerName,
                ManagerEmail = managerEmail,
                IsEnded = eventDate < now,
                IsFull = remaining <= 0 && eventDate >= now,
                IsOpen = isRegOpen && remaining > 0 && eventDate >= now
            };

            allEvents.Add(card);

            CountAll++;
            if (card.IsEnded)
            {
                CountEnded++;
            }
            else
            {
                CountUpcoming++;
                if (card.IsOpen) { CountOpen++; }
                if (card.IsFull) { CountFull++; }
            }
        }

        UpcomingEvents = CurrentFilter switch
        {
            "open" => allEvents.FindAll(e => e.IsOpen),
            "full" => allEvents.FindAll(e => e.IsFull),
            "ended" => allEvents.FindAll(e => e.IsEnded),
            "all" => allEvents,
            _ => allEvents.FindAll(e => !e.IsEnded)
        };
    }

    public class EventCard
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public int RemainingCapacity { get; set; }
        public bool IsRegistrationOpen { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;
        public bool IsEnded { get; set; }
        public bool IsFull { get; set; }
        public bool IsOpen { get; set; }
    }
}
