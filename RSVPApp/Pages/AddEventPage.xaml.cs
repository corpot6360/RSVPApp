using System;
using Microsoft.Maui.Controls;

namespace RSVPApp.Pages
{
    public partial class AddEventPage : ContentPage
    {
        public AddEventPage()
        {
            InitializeComponent();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(DescriptionEditor.Text) || DatePicker.Date == default)
            {
                await DisplayAlert("Validation", "Please fill out all fields.", "OK");
                return;
            }

            await DisplayAlert("Success", "Event added (not persisted).", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
