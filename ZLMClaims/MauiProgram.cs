using LocalizationResourceManager.Maui;
using Microsoft.Extensions.DependencyInjection;
// using MauiLocalizationResourceManagerSample.Resources;
using Microsoft.Extensions.Logging;
using ZLMClaims.Auth0;
using ZLMClaims.ViewModels;
using ZLMClaims.Views;

namespace ZLMClaims;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>() /* reference to app.xml */
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
        // addtrienmsient so you get a new instance of these pages everytime
        builder.Services.AddTransient<RepairCompanyViewModel>();
        builder.Services.AddTransient<RepairCompanyPage>();
        builder.Services.AddTransient<ContractViewModel>();
        builder.Services.AddTransient<ContractPage>();

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
