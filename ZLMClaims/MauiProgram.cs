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
        Console.WriteLine(DateTime.Now + "[..............] [MauiProgram] [CreateMauiApp] DI container");
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
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
       
        builder.Services.AddHttpClient<IRepairCompanyService, RepairCompanyService>();
        builder.Services.AddSingleton<AllRepairCompaniesViewModel>();
        builder.Services.AddTransient<AllRepairCompaniesPage>();
        builder.Services.AddTransient<RepairCompanyViewModel>();
        builder.Services.AddTransient<RepairCompanyPage>();

        builder.Services.AddHttpClient<IContractService, ContractService>();
        builder.Services.AddSingleton<AllContractsViewModel>();
        builder.Services.AddTransient<AllContractsPage>();
        builder.Services.AddTransient<ContractViewModel>();
        builder.Services.AddTransient<ContractPage>();

        builder.Services.AddHttpClient<IClaimFormService, ClaimFormService>();
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

        builder.Services.AddHttpClient<IUserService, UserService>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddTransient<UserPage>();

        // Auth0 registration
        Console.WriteLine(DateTime.Now + "[..............] [MauiProgram] [CreateMauiApp] Register Auth0Client");
        builder.Services.AddSingleton(new Auth0Client(new()
        {                      
            Domain = "dev-ajcve7wvqiq10doi.eu.auth0.com",
            ClientId = "kgYGjeSFKI7GczXNx335myCMXbdwx6UG",
            Scope = "openid profile",
            Audience = "https://zlmclaim-api.com",
#if WINDOWS
            RedirectUri = "https://localhost/callback"
#else
            RedirectUri = "zlmclaims://callback"
#endif
        }));
        // builder.Services.AddHttpClient("ZLMClaims",                
        // // client => client.BaseAddress = new Uri("https://localhost:7040")
        Console.WriteLine(DateTime.Now + "[..............] [MauiProgram] [CreateMauiApp] Register TokenHandler");
        builder.Services.AddSingleton<TokenHandler>();
        builder.Services.AddHttpClient("ZLMClaims",                
                client => client.BaseAddress = new Uri("https://10.0.2.2:7040 ")
            ).AddHttpMessageHandler<TokenHandler>();
        builder.Services.AddTransient(
            sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ZLMClaims")
        );

        return builder.Build();
	}
}