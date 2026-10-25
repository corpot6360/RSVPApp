namespace RSVPApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Data.DataStore.InitializeAsync();
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

            var user = await Data.DataStore.ValidateCredentialsAsync(username, password);
            if (user != null)
            {
                Data.DataStore.SignIn(user);
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
