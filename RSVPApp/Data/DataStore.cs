using System;
using System.Collections.Generic;
using System.Linq;
using RSVPApp.Models;

namespace RSVPApp.Data
{
    public static class DataStore
    {
        public static User? CurrentUser { get; private set; }

        private static readonly Dictionary<string, string> _credentials = new()
        {
            { "student", "password123" }
        };

        private static readonly List<Event> _events = new()
        {
            new Event { Title = "Welcome Mixer", Description = "Meet and greet for new students.", Date = DateTime.Now.AddDays(3), HostUsername = "student", Attendees = new List<string>{ "student" } },
            new Event { Title = "Study Group", Description = "Weekly study session.", Date = DateTime.Now.AddDays(7), HostUsername = "otherHost", Attendees = new List<string>{ "someone" } },
            new Event { Title = "Career Fair", Description = "Explore internship opportunities.", Date = DateTime.Now.AddDays(14), HostUsername = "companyHost", Attendees = new List<string>{  } }
        };

        public static bool ValidateCredentials(string username, string password)
        {
            if (_credentials.TryGetValue(username, out var pw))
            {
                return pw == password;
            }
            return false;
        }

        public static void SignIn(string username, string fullName, string email)
        {
            CurrentUser = new User { Username = username, FullName = fullName, Email = email, IsGuest = false };
        }

        public static void SignInGuest()
        {
            CurrentUser = new User { Username = "guest", FullName = "Guest", Email = string.Empty, IsGuest = true };
        }

        public static void SignOut()
        {
            CurrentUser = null;
        }

        public static IEnumerable<Event> GetAllEvents() => _events;

        public static IEnumerable<Event> GetEventsAttending(string username) => _events.Where(e => e.Attendees.Contains(username));

        public static IEnumerable<Event> GetEventsHosting(string username) => _events.Where(e => e.HostUsername == username);
    }
}
