using Microsoft.Maui.ApplicationModel.Communication;
using ZLMClaims.Auth0;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class LoginPage : ContentPage
{
    private readonly Auth0Client auth0Client;
    private UserService userService;
    public LoginPage(Auth0Client client)
	{
        InitializeComponent();
        auth0Client = client;

#if WINDOWS
        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif

        userService = new UserService(new HttpClient());
    }


    public event EventHandler<bool> LoginStatusChanged;


    // Auth0 login button
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
            TokenHolder.AccessToken = loginResult.AccessToken;

            await Task.Delay(1000); 
            User user = await userService.GetUserByEmailAsync(loginResult.User.Identity.Name);            
            LoginStatusChanged?.Invoke(this, true); 

            // Saving the userid
            Preferences.Default.Set("userId", user.Id);
        }
        else
        {
            LoginStatusChanged?.Invoke(this, false); 
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

    // Auth0 logout button 
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var logoutResult = await auth0Client.LogoutAsync();

        if (!logoutResult.IsError)
        {
            HomeView.IsVisible = false;
            LoginView.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }
    }

    private async void OnLoaded(object sender, EventArgs e)
    {
        var user = await auth0Client.GetAuthenticatedUser();

        if (user != null)
        {            
            UsernameLbl.Text = user.Identity.Name;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
            LoginStatusChanged?.Invoke(this, true); 
        }
    }
}

