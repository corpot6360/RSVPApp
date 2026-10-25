using System;
using System.Linq;
using Microsoft.Maui.Controls;
using RSVPApp.Data;
using RSVPApp.Models;

namespace RSVPApp.Pages
{
    public partial class EventsPage : ContentPage
    {
        private class EventListItem
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Date { get; set; } = string.Empty;
        }

        public EventsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAllAsync();
        }

        private static EventListItem ToListItem(Event e)
        {
            return new EventListItem
            {
                Id = e.Id,
                Title = e.Title,
                Date = e.Date.ToString("g")
            };
        }

        private async Task LoadAllAsync()
        {
            var events = await DataStore.GetAllEventsAsync();
            EventsCollection.ItemsSource = events.Select(ToListItem).ToList();
        }

        private async Task LoadAttendingAsync()
        {
            var user = DataStore.CurrentUser;
            if (user == null || user.IsGuest)
            {
                EventsCollection.ItemsSource = Array.Empty<object>();
                return;
            }

            var events = await DataStore.GetEventsAttendingAsync(user.Id);
            EventsCollection.ItemsSource = events.Select(ToListItem).ToList();
        }

        private async Task LoadHostingAsync()
        {
            var user = DataStore.CurrentUser;
            if (user == null || user.IsGuest)
            {
                EventsCollection.ItemsSource = Array.Empty<object>();
                return;
            }

            var events = await DataStore.GetEventsHostingAsync(user.Id);
            EventsCollection.ItemsSource = events.Select(ToListItem).ToList();
        }

        private async void OnAllClicked(object sender, EventArgs e) => await LoadAllAsync();
        private async void OnAttendingClicked(object sender, EventArgs e) => await LoadAttendingAsync();
        private async void OnHostingClicked(object sender, EventArgs e) => await LoadHostingAsync();

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                if (e.CurrentSelection[0] is EventListItem item)
                {
                    await Shell.Current.GoToAsync($"eventdetails?eventId={item.Id}");
                }
                EventsCollection.SelectedItem = null;
            }
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("addevent");
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            DataStore.SignOut();
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
