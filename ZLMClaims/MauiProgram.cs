using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using ZLMClaims.Auth0;
using ZLMClaims.Services;
using ZLMClaims.ViewModels;
using ZLMClaims.Views;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Devices.Sensors;

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
            })
            .UseMauiMaps();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPage>();

        // Register all with the Dependency Injection container

        //string dbPath = Path.Combine(FileSystem.AppDataDirectory, "zlmclaims.db3");    

        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
       

        builder.Services.AddHttpClient<IRepairCompanyService, RepairCompanyService>();
        builder.Services.AddSingleton<AllRepairCompaniesViewModel>();
        builder.Services.AddTransient<AllRepairCompaniesPage>();
        builder.Services.AddTransient<RepairCompanyViewModel>();
        builder.Services.AddTransient<RepairCompanyPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** RepairCompany service, viewmodel and view registered with DI container ");

        builder.Services.AddHttpClient<IContractService, ContractService>();
        builder.Services.AddSingleton<AllContractsViewModel>();
        builder.Services.AddTransient<AllContractsPage>();
        builder.Services.AddTransient<ContractViewModel>();
        builder.Services.AddTransient<ContractPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** Contract service, viewmodel and view registered with DI container ");

        builder.Services.AddHttpClient<IClaimService, ClaimService>();
        builder.Services.AddSingleton<ILocalClaimService, LocalClaimService>();
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<LocalClaimService> (s, dbPath));
        builder.Services.AddSingleton<AllClaimsViewModel>();
        builder.Services.AddTransient<AllClaimsPage>();
        builder.Services.AddTransient<ClaimFormStep1Page>();
        builder.Services.AddTransient<ClaimFormStep2Page>();
        builder.Services.AddTransient<ClaimFormStep3Page>();
        builder.Services.AddTransient<ClaimFormStep4Page>();
        builder.Services.AddTransient<ClaimFormStep5Page>();
        builder.Services.AddTransient<ClaimFormStep1ViewModel>();
        builder.Services.AddTransient<ClaimFormStep2ViewModel>();
        builder.Services.AddTransient<ClaimFormStep3ViewModel>();
        builder.Services.AddTransient<ClaimFormStep4ViewModel>();
        builder.Services.AddTransient<ClaimFormStep5ViewModel>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** Claim service, viewmodel and view registered with DI container ");

        builder.Services.AddHttpClient<IUserService, UserService>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddTransient<UserPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserService, UserViewModel and UserPage registered with DI container ");

        builder.Services.AddSingleton<ICarService, CarService>();
        builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddTransient<AllCarsPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** CarService, viewmodel and view registered with DI container ");

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