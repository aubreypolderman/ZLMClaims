using LocalizationResourceManager.Maui;
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
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
		/*	.UseLocalizationResourceManager(settings =>
			{
				settings.RestoreLatestCulture(true);
				settings.AddResource(AppResources.ResourceManager);
			}) */
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();

        // Dependeny injection
        // AddTransient so you get a new instance of these pages everytime
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
        builder.Services.AddSingleton<IUserService, UserService>();

        builder.Services.AddTransient<RepairCompanyService>();
        builder.Services.AddTransient<RepairCompanyViewModel>();
        builder.Services.AddTransient<RepairCompanyPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** RepairCompanyService, AllCompaniesViewModel and AllCompaniesPage injected ");

        builder.Services.AddTransient<ContractViewModel>();
        builder.Services.AddTransient<ContractPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** ContractPage injected ");

        builder.Services.AddSingleton<UserService>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<UserPage>();
        Console.WriteLine("[..............] [MauiProgram] [MauiApp] ****** UserService, UserViewModel and UserPage injected ");

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
