using System;
using System.Linq;
using Microsoft.Maui.Controls;
using RSVPApp.Data;

namespace RSVPApp.Pages
{
    [QueryProperty(nameof(EventId), "eventId")]
    public partial class RsvpPage : ContentPage
    {
        public string EventId { get; set; }

        public RsvpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadEvent();
            PrepopulateIfLoggedIn();
        }

        private void LoadEvent()
        {
            if (string.IsNullOrEmpty(EventId))
                return;

            var ev = DataStore.GetAllEvents().FirstOrDefault(e => e.Id == EventId);
            if (ev != null)
            {
                EventTitleLabel.Text = ev.Title;
            }
        }

        private void PrepopulateIfLoggedIn()
        {
            var user = DataStore.CurrentUser;
            if (user != null && !user.IsGuest)
            {
                NameEntry.Text = user.FullName;
                EmailEntry.Text = user.Email;
            }
            else
            {
                NameEntry.Text = string.Empty;
                EmailEntry.Text = string.Empty;
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(GuestsEntry.Text))
            {
                await DisplayAlert("Validation", "Please fill out all fields.", "OK");
                return;
            }

            await DisplayAlert("Saved", "RSVP saved (not persisted).", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
