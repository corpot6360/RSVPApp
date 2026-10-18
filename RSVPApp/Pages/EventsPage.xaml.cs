using System;
using System.Linq;
using Microsoft.Maui.Controls;
using RSVPApp.Data;

namespace RSVPApp.Pages
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
            LoadAll();
        }

        private void LoadAll()
        {
            EventsCollection.ItemsSource = DataStore.GetAllEvents().Select(e => new { e.Id, e.Title, Date = e.Date.ToString("g") });
        }

        private void LoadAttending()
        {
            var user = DataStore.CurrentUser;
            if (user == null)
            {
                EventsCollection.ItemsSource = Array.Empty<object>();
                return;
            }
            EventsCollection.ItemsSource = DataStore.GetEventsAttending(user.Username).Select(e => new { e.Id, e.Title, Date = e.Date.ToString("g") });
        }

        private void LoadHosting()
        {
            var user = DataStore.CurrentUser;
            if (user == null)
            {
                EventsCollection.ItemsSource = Array.Empty<object>();
                return;
            }
            EventsCollection.ItemsSource = DataStore.GetEventsHosting(user.Username).Select(e => new { e.Id, e.Title, Date = e.Date.ToString("g") });
        }

        private void OnAllClicked(object sender, EventArgs e) => LoadAll();
        private void OnAttendingClicked(object sender, EventArgs e) => LoadAttending();
        private void OnHostingClicked(object sender, EventArgs e) => LoadHosting();

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var item = e.CurrentSelection[0];
                var idProp = item.GetType().GetProperty("Id");
                var id = idProp?.GetValue(item)?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    await Shell.Current.GoToAsync($"eventdetails?eventId={id}");
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
