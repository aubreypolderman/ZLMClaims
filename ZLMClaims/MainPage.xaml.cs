using ZLMClaims.Auth0;

namespace ZLMClaims;

public partial class MainPage : ContentPage
{
	int count = 0;
    private readonly Auth0Client auth0Client;

    public MainPage(Auth0Client client)
	{
		InitializeComponent();
        auth0Client = client;

        #if WINDOWS
            auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
        #endif
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    
    // Auth0 login button
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [MainPage] [OnLoginClicked] start");
        var loginResult = await auth0Client.LoginAsync();
        Console.WriteLine(DateTime.Now + "[..............] [MainPage] [OnLoginClicked] na uitvoer LoginAsync met result: " + loginResult);

        if (!loginResult.IsError)
        {
            Console.WriteLine(DateTime.Now + "[..............] [MainPage] [OnLoginClicked] Succces, with username: " + loginResult.User.Identity.Name);
            UsernameLbl.Text = loginResult.User.Identity.Name;
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
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
            UsernameLbl.Text = user.Identity.Name;
            //UserPictureImg.Source = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
    }
}

