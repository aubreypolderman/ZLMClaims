using ZLMClaims.Auth0;
using ZLMClaims.Services;

namespace ZLMClaims;

public partial class App : Application
{
    /*
	public static LocalClaimService LocalClaimService { get; private set; }
	public App(LocalClaimService localClaimService)
	{
		InitializeComponent();

		MainPage = new AppShell();
		LocalClaimService = localClaimService;

    }
	*/
    public App(Auth0Client auth0Client, Auth0Client auth0Clientauth0Client)
    {
        InitializeComponent();

        Console.WriteLine(DateTime.Now + "[..............] [App] [Constructor] Na InitializeComponent");
        MainPage = new AppShell(auth0Clientauth0Client);
        Console.WriteLine(DateTime.Now + "[..............] [App] [Constructor] Na uitvoer new AppShell(auth0Clientauth0Client)");


    }
}
