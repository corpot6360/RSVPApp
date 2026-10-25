using System;
using Microsoft.Maui.Controls;
using RSVPApp.Data;

namespace RSVPApp.Pages
{
    [QueryProperty(nameof(EventId), "eventId")]
    public partial class EventDetailsPage : ContentPage
    {
        public string EventId { get; set; }

        public EventDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadEvent();
        }

        private void LoadEvent()
        {
            if (string.IsNullOrEmpty(EventId))
                return;

            if (!int.TryParse(EventId, out var eventId))
                return;

            var ev = DataStore.GetEventByIdAsync(eventId).GetAwaiter().GetResult();
            if (ev == null)
                return;

            TitleLabel.Text = ev.Title;
            DateLabel.Text = ev.Date.ToString("f");
            HostLabel.Text = $"Host: {ev.HostUsername}";
            DescriptionLabel.Text = ev.Description;
        }

        private async void OnRsvpClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"rsvp?eventId={EventId}");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
