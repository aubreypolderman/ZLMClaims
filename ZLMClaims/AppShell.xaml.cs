namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Console.WriteLine("[..............] [AppShell] ****** INIT ****** ");

        // The localizationResourceManager uses binding, so the context needs to be set
        BindingContext = this;

// register route to details page
		Routing.RegisterRoute(nameof(Views.RepairCompanyPage), typeof(Views.RepairCompanyPage));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
        Routing.RegisterRoute(nameof(Views.ContractPage), typeof(Views.ContractPage));
    }

	public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;
}
