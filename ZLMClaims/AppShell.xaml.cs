using ZLMClaims.Auth0;
using ZLMClaims.Views;

namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell(Auth0Client auth0Client)
	{
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] init");
        InitializeComponent();
        var loginPage = new LoginPage(auth0Client);
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] loginPage instantiated");
        loginPage.LoginStatusChanged += OnLoginStatusChanged;
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] loginPage.LoginStatusChanged invoked");

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
        Routing.RegisterRoute(nameof(Views.AllClaimsPage), typeof(Views.AllClaimsPage));
        Routing.RegisterRoute(nameof(Views.AllContractsPage), typeof(Views.AllContractsPage));
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] All routes registered");
    }

    private void OnLoginStatusChanged(object sender, bool isLoggedIn)
    {   
        IsUserLoggedIn = isLoggedIn;
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [OnLoginStatusChanged] isLoggedIn: " + isLoggedIn);
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [OnLoginStatusChanged] IsUserLoggedIn: " + IsUserLoggedIn);
    }

    public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;

    public bool IsUserLoggedIn { get; set; } = true;
}
