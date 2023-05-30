using ZLMClaims.Auth0;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class LoginPage : ContentPage
{
	int count = 0;
    private readonly Auth0Client auth0Client;

    public LoginPage(Auth0Client client)
	{
		InitializeComponent();
        auth0Client = client;

#if WINDOWS
        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
    }


    // Auth0 login button
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] start");
        var loginResult = await auth0Client.LoginAsync();
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] na uitvoer LoginAsync met result: " + loginResult);

        if (!loginResult.IsError)
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Succces, with username: " + loginResult.User.Identity.Name);
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
            int userId = 1;
            
            // Saving the userid
            Preferences.Default.Set("userId", userId);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] Preferenceset updated with userid " + userId);

        }
        else
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [OnLoginClicked] No Succces");
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
        Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user invoked");

        if (user != null)
        {
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user => " + user);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user => " + user.ToString);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user GetType => " + user.GetType());
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] userIdentities => " + user.Identities);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user Claims=> " + user.Claims);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user IsInRole =>  " + user.IsInRole);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user Name => " + user.Identity.Name);
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] user IdType => " + user.Identity.AuthenticationType);
            UsernameLbl.Text = user.Identity.Name;
            //UserPictureImg.Source = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
    }
}

