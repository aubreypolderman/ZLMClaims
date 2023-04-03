namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register route to step1 so navigation can take place
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
    }
}
