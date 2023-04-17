using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using ZLMClaims.Auth0;
using ZLMClaims.Services;
using ZLMClaims.ViewModels;
using ZLMClaims.Views;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace ZLMClaims;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();

        // Register all with the Dependency Injection container
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
       

        builder.Services.AddSingleton<IRepairCompanyService, RepairCompanyService>();
        builder.Services.AddTransient<AllRepairCompaniesViewModel>();
        builder.Services.AddTransient<RepairCompanyViewModel>();
        builder.Services.AddTransient<RepairCompanyPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** RepairCompany service, viewmodel and view registered with DI container ");

       // builder.Services.AddSingleton<IContractService, ContractService>();
        builder.Services.AddHttpClient<IContractService, ContractService>();
        builder.Services.AddTransient<AllContractsViewModel>();
        builder.Services.AddTransient<AllContractsPage>();
        builder.Services.AddTransient<ContractViewModel>();
        builder.Services.AddTransient<ContractPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** Contract service, viewmodel and view registered with DI container ");

        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<UserPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserService, UserViewModel and UserPage registered with DI container ");

        // Auth0 registration
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "dev-ajcve7wvqiq10doi.eu.auth0.com",
            ClientId = "kgYGjeSFKI7GczXNx335myCMXbdwx6UG",
            Scope = "openid profile",
            RedirectUri = "myapp://callback"
        }));

        return builder.Build();
	}
}