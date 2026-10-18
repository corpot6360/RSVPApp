namespace RSVPApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim() ?? string.Empty;
            var password = PasswordEntry.Text ?? string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Validation", "Please enter username and password.", "OK");
                return;
            }

            if (Data.DataStore.ValidateCredentials(username, password))
            {
                // sign in with some basic info (student-level app)
                Data.DataStore.SignIn(username, "Student User", username + "@school.edu");
                await Shell.Current.GoToAsync("events");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid credentials.", "OK");
            }
        }

        private async void OnGuestClicked(object sender, EventArgs e)
        {
            Data.DataStore.SignInGuest();
            await Shell.Current.GoToAsync("events");
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("adduser");
        }

        private async void OnViewEventsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("events");
        }
    }
}
