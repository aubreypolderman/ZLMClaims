using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using ZLMClaims.Auth0;
using ZLMClaims.Views;

namespace ZLMClaims;

public partial class AppShell : Shell
{
    public bool IsLoginVisible { get; set; }
    public event EventHandler<bool> LoginStatusChanged;

    public AppShell(Auth0Client auth0Client)
	{
        // todo: set by method
        IsLoginVisible = true;
        IsVisible = true;
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] init Set IsLoginVisible=true and IsVisible=true");
        InitializeComponent();
        var loginPage = new LoginPage(auth0Client);
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] LoginPage instantiated");
        loginPage.LoginStatusChanged += OnLoginStatusChanged;
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] LoginPage.LoginStatusChanged invoked");
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [Constructor] LoginStatusChanged => " + LoginStatusChanged);
       

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

    private void OnLoginStatusChanged(object sender, bool isVisible)
    {
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [OnLoginStatusChanged] init");
        IsVisible = isVisible;
        if (IsVisible) { IsLoginVisible = false; }
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [OnLoginStatusChanged] isLoggedIn: " + isVisible);
        Console.WriteLine(DateTime.Now + "[..............] [AppShell] [OnLoginStatusChanged] IsUserLoggedIn: " + IsLoginVisible);

        // nieuwe code
        if (IsVisible)
        {
            // 
            IsLoginVisible = false;
            // De gebruiker is ingelogd, bottombar navigatie zichtbaar maken
            foreach (var item in Items)
            {
                if (item is ShellItem shellItem)
                {
                    foreach (var section in shellItem.Items)
                    {
                        if (section is ShellSection shellSection)
                        {
                            foreach (var content in shellSection.Items)
                            {
                                if (content is ShellContent shellContent)
                                {
                                    shellContent.IsVisible = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // De gebruiker is uitgelogd, bottombar navigatie verbergen
            foreach (var item in Items)
            {
                if (item is ShellItem shellItem)
                {
                    foreach (var section in shellItem.Items)
                    {
                        if (section is ShellSection shellSection)
                        {
                            foreach (var content in shellSection.Items)
                            {
                                if (content is ShellContent shellContent)
                                {
                                    shellContent.IsVisible = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;

}
