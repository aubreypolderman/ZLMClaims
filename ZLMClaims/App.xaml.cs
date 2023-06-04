using ZLMClaims.Auth0;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims;

public partial class App : Application
{
    public App(Auth0Client auth0Client, Auth0Client auth0Clientauth0Client)
    {
        InitializeComponent();

        Console.WriteLine(DateTime.Now + "[..............] [App] [Constructor] Na InitializeComponent");
        //MainPage = new LoginPage(auth0Clientauth0Client);
        MainPage = new AppShell(auth0Clientauth0Client);
        Console.WriteLine(DateTime.Now + "[..............] [App] [Constructor] Na uitvoer new AppShell(auth0Clientauth0Client)");


    }
}
