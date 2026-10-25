using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RSVPApp.Models;

namespace RSVPApp.Data
{
    public static class DataStore
    {
        public static User? CurrentUser { get; private set; }

        private static bool _initialized;
        private static readonly SemaphoreSlim _initLock = new(1, 1);

        public static async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            await _initLock.WaitAsync();
            try
            {
                if (_initialized)
                {
                    return;
                }

                var db = await DatabaseService.GetDatabaseAsync();

                var existingUserCount = await db.Table<User>().CountAsync();
                if (existingUserCount == 0)
                {
                    var seedUser = new User
                    {
                        Username = "student",
                        Password = "password123",
                        FullName = "Student User",
                        Email = "student@school.edu",
                        IsGuest = false
                    };
                    await db.InsertAsync(seedUser);

                    var seedEvents = new List<Event>
                    {
                        new() { Title = "Welcome Mixer", Description = "Meet and greet for new students.", Date = DateTime.Now.AddDays(3), HostUserId = seedUser.Id, HostUsername = seedUser.Username },
                        new() { Title = "Study Group", Description = "Weekly study session.", Date = DateTime.Now.AddDays(7), HostUserId = seedUser.Id, HostUsername = seedUser.Username },
                        new() { Title = "Career Fair", Description = "Explore internship opportunities.", Date = DateTime.Now.AddDays(14), HostUserId = seedUser.Id, HostUsername = seedUser.Username }
                    };

                    await db.InsertAllAsync(seedEvents);

                    var attendingRsvp = new Rsvp
                    {
                        EventId = seedEvents[0].Id,
                        UserId = seedUser.Id,
                        AttendeeName = seedUser.FullName,
                        AttendeeEmail = seedUser.Email,
                        GuestCount = 0
                    };
                    await db.InsertAsync(attendingRsvp);
                }

                _initialized = true;
            }
            finally
            {
                _initLock.Release();
            }
        }

        public static async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            return await db.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public static async Task<bool> AddUserAsync(string username, string password, string fullName, string email)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();

            var existing = await db.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
            if (existing != null)
            {
                return false;
            }

            var user = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Email = email,
                IsGuest = false
            };

            await db.InsertAsync(user);
            return true;
        }

        public static void SignIn(User user)
        {
            CurrentUser = user;
        }

        public static void SignInGuest()
        {
            CurrentUser = new User { Id = 0, Username = "guest", FullName = "Guest", Email = string.Empty, IsGuest = true };
        }

        public static void SignOut()
        {
            CurrentUser = null;
        }

        public static async Task<List<Event>> GetAllEventsAsync()
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            return await db.Table<Event>().OrderBy(e => e.Date).ToListAsync();
        }

        public static async Task<List<Event>> GetEventsHostingAsync(int userId)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            return await db.Table<Event>().Where(e => e.HostUserId == userId).OrderBy(e => e.Date).ToListAsync();
        }

        public static async Task<List<Event>> GetEventsAttendingAsync(int userId)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();

            var rsvps = await db.Table<Rsvp>().Where(r => r.UserId == userId).ToListAsync();
            if (rsvps.Count == 0)
            {
                return new List<Event>();
            }

            var eventIds = rsvps.Select(r => r.EventId).Distinct().ToList();
            var allEvents = await db.Table<Event>().ToListAsync();
            return allEvents
                .Where(e => eventIds.Contains(e.Id))
                .OrderBy(e => e.Date)
                .ToList();
        }

        public static async Task<Event?> GetEventByIdAsync(int eventId)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            return await db.Table<Event>().Where(e => e.Id == eventId).FirstOrDefaultAsync();
        }

        public static async Task AddEventAsync(Event evt)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            await db.InsertAsync(evt);
        }

        public static async Task AddRsvpAsync(Rsvp rsvp)
        {
            await InitializeAsync();
            var db = await DatabaseService.GetDatabaseAsync();
            await db.InsertAsync(rsvp);
        }
    }
}
