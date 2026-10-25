using SQLite;

namespace RSVPApp.Models
{
    public class Rsvp
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int EventId { get; set; }
        public int? UserId { get; set; }

        public string AttendeeName { get; set; } = string.Empty;
        public string AttendeeEmail { get; set; } = string.Empty;
        public int GuestCount { get; set; }
    }
}
