using System;
using System.Collections.Generic;

namespace RSVPApp.Models
{
    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string HostUsername { get; set; } = string.Empty;
        public List<string> Attendees { get; set; } = new List<string>();
    }
}
