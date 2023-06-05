using Microsoft.Maui.ApplicationModel.Communication;
using ZLMClaims.Auth0;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class LoginPage : ContentPage
{
	int count = 0;
    private readonly Auth0Client auth0Client;
    private UserService userService;
    public LoginPage(Auth0Client client)
	{
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [Constructor] init");
        InitializeComponent();
        auth0Client = client;

#if WINDOWS
        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif

        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [Constructor] before invoke UserService");
        userService = new UserService(new HttpClient());
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [Constructor] after invoke UserService");
    }


    public event EventHandler<bool> LoginStatusChanged;


    // Auth0 login button
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] init");
        var loginResult = await auth0Client.LoginAsync();
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] na uitvoer LoginAsync met result: " + loginResult.ToString());

        if (!loginResult.IsError)
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Succces, with username: " + loginResult.User.Identity.Name);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Succces, Identity.AuthenticationType: " + loginResult.User.Identity.AuthenticationType);            
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Succces, IsAuthenticated: " + loginResult.User.Identity.IsAuthenticated);
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
            TokenHolder.AccessToken = loginResult.AccessToken;

            // Retrieve userId of the user given the emailaddress / username
            User user = await userService.GetUserByEmailAsync(loginResult.User.Identity.Name);            

            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] invoke LoginStatusChanged met true");
            LoginStatusChanged?.Invoke(this, true); // Geef aan dat de gebruiker is ingelogd
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] na invoke LoginStatusChanged met true");

            // Saving the userid
            Preferences.Default.Set("userId", user.Id);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Preferenceset updated with userid " + user.Id);

        }
        else
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] invoke LoginStatusChanged met false");
            LoginStatusChanged?.Invoke(this, false); // Geef aan dat de gebruiker niet is ingelogd            

            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] invoke LoginStatusChanged na false");
        }
    }

    // Auth0 logout button 
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLogoutClicked] init");
        var logoutResult = await auth0Client.LogoutAsync();

        if (!logoutResult.IsError)
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLogoutClicked] Set LoginVoew.IsVisible to true");
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
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoaded] init");
        var user = await auth0Client.GetAuthenticatedUser();
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoaded] after invoke auth0Client.GetAuthenticatedUser");

        if (user != null)
        {            
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] retrieved identityname => " + user.Identity.Name);            
            UsernameLbl.Text = user.Identity.Name;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
            LoginStatusChanged?.Invoke(this, true); // Geef aan dat de gebruiker is ingelogd  
        }
    }
}

