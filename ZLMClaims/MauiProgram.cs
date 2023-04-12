using LocalizationResourceManager.Maui;
using Microsoft.Extensions.DependencyInjection;
// using MauiLocalizationResourceManagerSample.Resources;
using Microsoft.Extensions.Logging;
using ZLMClaims.Auth0;
using ZLMClaims.Services;
using ZLMClaims.ViewModels;
using ZLMClaims.Views;

namespace ZLMClaims;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** INIT ****** ");
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>() /* reference to app.xml */
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        
        builder.Services.AddSingleton<MainPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** MainPage injected ");

        // Dependeny injection
        // addtrienmsient so you get a new instance of these pages everytime
        builder.Services.AddTransient<RepairCompanyViewModel>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** RepairCompanyViewModel injected ");
        builder.Services.AddTransient<RepairCompanyPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** RepairCompanyPage injected ");
        builder.Services.AddTransient<ContractViewModel>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** ContractViewModel injected ");
        builder.Services.AddTransient<ContractPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** ContractPage injected ");
        builder.Services.AddTransient<UserViewModel>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserViewModel injected ");
        builder.Services.AddTransient<UserPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserPage injected ");
        builder.Services.AddTransient<RepairCompanyService>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserService injected ");

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
