using ZLMClaims.Services;

namespace ZLMClaims;

public partial class App : Application
{
	public static LocalClaimService LocalClaimService { get; private set; }
	public App(LocalClaimService localClaimService)
	{
		InitializeComponent();

		MainPage = new AppShell();
		LocalClaimService = localClaimService;

    }
}
