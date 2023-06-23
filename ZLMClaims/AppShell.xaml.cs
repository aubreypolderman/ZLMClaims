using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using ZLMClaims.Auth0;
using ZLMClaims.Views;

namespace ZLMClaims;

public partial class AppShell : Shell
{
    public event EventHandler<bool> LoginStatusChanged;

    public AppShell(Auth0Client auth0Client)
	{
        InitializeComponent();

        // register route to details page
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep2Page), typeof(Views.ClaimFormStep2Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep3Page), typeof(Views.ClaimFormStep3Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep4Page), typeof(Views.ClaimFormStep4Page));
        Routing.RegisterRoute(nameof(Views.ClaimFormStep5Page), typeof(Views.ClaimFormStep5Page));
        Routing.RegisterRoute(nameof(Views.ContractPage), typeof(Views.ContractPage));
        Routing.RegisterRoute(nameof(Views.AllClaimsPage), typeof(Views.AllClaimsPage));
        Routing.RegisterRoute(nameof(Views.AllContractsPage), typeof(Views.AllContractsPage));
    }

    public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;

}
