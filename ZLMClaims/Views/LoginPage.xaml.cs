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
            Console.WriteLine(DateTime.Now + "[..............] [LoginPage] [GetAuthenticatedUser] retrieved identityname => " + user.Identity.Name);            
            UsernameLbl.Text = user.Identity.Name;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
    }
}

