using System;
using Microsoft.Maui.Controls;
using RSVPApp.Data;

namespace RSVPApp.Pages
{
    public partial class AddUserPage : ContentPage
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameEntry.Text) || string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                await DisplayAlert("Validation", "Please fill out all fields.", "OK");
                return;
            }

            var pwd = PasswordEntry.Text ?? string.Empty;
            var confirm = ConfirmPasswordEntry.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(pwd) || string.IsNullOrWhiteSpace(confirm))
            {
                await DisplayAlert("Validation", "Please enter and confirm the password.", "OK");
                return;
            }

            if (pwd != confirm)
            {
                await DisplayAlert("Validation", "Passwords do not match.", "OK");
                return;
            }

            await DisplayAlert("Success", "User added.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
