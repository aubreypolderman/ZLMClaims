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
    }

   
    // Auth0 login button
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [LoginPage] [OnLoginClicked] start");
        var loginResult = await auth0Client.LoginAsync();
        Console.WriteLine("[..............] [LoginPage] [OnLoginClicked] na uitvoer LoginAsync met result: " + loginResult);

        if (!loginResult.IsError)
        {
            Console.WriteLine("[..............] [LoginPage] [OnLoginClicked] Succces, with username: " + loginResult.User.Identity.Name);
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
        else
        {
            Console.WriteLine("[..............] [LoginPage] [OnLoginClicked] No Succces");
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
}

