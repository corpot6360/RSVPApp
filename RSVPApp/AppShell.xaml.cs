namespace RSVPApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("events", typeof(Pages.EventsPage));
            Routing.RegisterRoute("adduser", typeof(Pages.AddUserPage));
            Routing.RegisterRoute("addevent", typeof(Pages.AddEventPage));
            Routing.RegisterRoute("eventdetails", typeof(Pages.EventDetailsPage));
            Routing.RegisterRoute("rsvp", typeof(Pages.RsvpPage));
        }
    }
}
