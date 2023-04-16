using ZLMClaims.ViewModels;
using ZLMClaims.Services;
using Microsoft.Extensions.DependencyInjection;
namespace ZLMClaims;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        // MainPage = new AppShell();
        
        Console.WriteLine("[..............] [App] ****** INIT ****** ");
        Console.WriteLine("[..............] [App] ****** Voor serviceprovider ****** ");

        // Register httpclient and UserService implementation
        var serviceProvider = new ServiceCollection()
               .AddHttpClient()
               .AddTransient<IUserService, UserService>()
               .AddTransient<IRepairCompanyService, RepairCompanyService>()
               .AddTransient<UserViewModel>()
               .BuildServiceProvider();
        Console.WriteLine("[..............] [App] ****** Na serviceprovider ****** ");

        MainPage = new AppShell { BindingContext = serviceProvider.GetService<UserViewModel>() };
        // MainPage = new AppShell();
        Console.WriteLine("[..............] [App] ****** MainPage geladen met new AppShell met MainPage " + MainPage);
     
    }
}
