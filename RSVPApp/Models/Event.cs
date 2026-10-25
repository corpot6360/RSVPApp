using System;
using SQLite;

namespace RSVPApp.Models
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public int HostUserId { get; set; }
        public string HostUsername { get; set; } = string.Empty;
    }
}
