namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Console.WriteLine("[..............] [AppShell] ****** INIT ****** ");

        // The localizationResourceManager uses binding, so the context needs to be set
        BindingContext = this;
        Console.WriteLine("[..............] [AppShell] ****** BindingContext {BindingContext} gezet ****** ");

        // register route to details page of the repaircompany
        Routing.RegisterRoute(nameof(Views.RepairCompanyPage), typeof(Views.RepairCompanyPage));
        Console.WriteLine("[..............] [AppShell] ****** RepairCompanyPage geregistreerd ****** ");

        // register route to details page of the contract
        Routing.RegisterRoute(nameof(Views.ContractPage), typeof(Views.ContractPage));
        Console.WriteLine("[..............] [AppShell] ****** ContractPage geregistreerd ****** ");

        // Register route to step1 so navigation can take place
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
        Console.WriteLine("[..............] [AppShell] ****** ClaimFormStep1Page geregistreerd ****** ");
    }

	public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;
}
