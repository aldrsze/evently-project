using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages;

public class IndexModel : PageModel
{
    public List<EventCard> UpcomingEvents { get; set; } = new();

    public void OnGet()
    {
        DataTable table = EventService.GetEvent();
        foreach (DataRow row in table.Rows)
        {
            int eventId = Convert.ToInt32(row["event_id"]);

            UpcomingEvents.Add(new EventCard
            {
                EventId = eventId,
                Name = row["name"].ToString() ?? "",
                Venue = row["venue"].ToString() ?? "",
                Description = row["description"].ToString() ?? "",
                EventDate = DateTime.TryParse(row["event_date"].ToString(), out DateTime dt) ? dt : DateTime.Now,
                Capacity = Convert.ToInt32(row["capacity"]),
                IsRegistrationOpen = EventService.IsRegistrationOpen(eventId)
            });
        }
    }

    public class EventCard
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public bool IsRegistrationOpen { get; set; }
    }
}
