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

            var created = await DataStore.AddUserAsync(
                UsernameEntry.Text!.Trim(),
                pwd,
                FullNameEntry.Text!.Trim(),
                EmailEntry.Text!.Trim());

            if (!created)
            {
                await DisplayAlert("Validation", "That username already exists.", "OK");
                return;
            }

            await DisplayAlert("Success", "User account created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
