using IdentityModel.OidcClient;
using ZLMClaims.Auth0;    // 👈 new code
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.Views;

public partial class LoginPage : ContentPage
{
    private readonly Auth0Client auth0Client;
    private UserService userService;
  
    public LoginPage(Auth0Client client)
    {
        InitializeComponent();
        auth0Client = client;


        // 👇 auth0 code
#if WINDOWS
    auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
        // 👆 auth0 code
        userService = new UserService(new HttpClient());
    }



    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;

            TokenHolder.AccessToken = loginResult.AccessToken;
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("///home");

            // Get and save userid
            User user = await userService.GetUserByEmailAsync(loginResult.User.Identity.Name);
            await SecureStorage.SetAsync("userId", user.Id.ToString());
            
        }
        else
        {
            await DisplayAlert("Error", "An exception occurred while logging in: " + loginResult.ErrorDescription, "OK");
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
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync("///login");
        }
        else
        {
            await DisplayAlert("Error", "An exception occurred while logging out: " + logoutResult.ErrorDescription, "OK");
        }
    }
}

