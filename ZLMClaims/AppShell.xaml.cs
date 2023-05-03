namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// The localizationResourceManager uses binding, so the context needs to be set
		BindingContext = this;

		// register route to details page
		Routing.RegisterRoute(nameof(Views.RepairCompanyPage), typeof(Views.RepairCompanyPage));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep2Page), typeof(Views.ClaimFormStep2Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep3Page), typeof(Views.ClaimFormStep3Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep4Page), typeof(Views.ClaimFormStep4Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep5Page), typeof(Views.ClaimFormStep5Page));
        Routing.RegisterRoute(nameof(Views.ContractPage), typeof(Views.ContractPage));
    }

	public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;
}
