namespace RSVPApp.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsGuest { get; set; }
    }
}
