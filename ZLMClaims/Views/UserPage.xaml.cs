using Microsoft.Maui.ApplicationModel;
using System.Globalization;
using ZLMClaims.Resources.Languages;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class UserPage : ContentPage
{      
    private readonly UserViewModel _viewModel;

    public UserPage() 
    {
        Console.WriteLine("[UserPage] [noargs constructor] [==============] Start");
        Console.WriteLine("[UserPage] [noargs constructor] [==============] Before InitializeComponent");
        InitializeComponent();
        Console.WriteLine("[UserPage] [noargs constructor] [==============] After InitializeComponent");

        Console.WriteLine("[UserPage] [noargs constructor] [==============] Before instantiation of UserViewModel");
        _viewModel = new UserViewModel();
        Console.WriteLine("[UserPage] [noargs constructor] [==============] After instantiation of UserViewModel");
        Console.WriteLine("[UserPage] [noargs constructor] [==============] Before call GetUser with hardcoded id 1");
        _viewModel.GetUser(1);
        Console.WriteLine("[UserPage] [noargs constructor] [==============] After call GetUser with hardcoded id 1");

        Console.WriteLine("[UserPage] [noargs constructor] [==============] Before bindingContext with _viewmodel");
        BindingContext = _viewModel;
        Console.WriteLine("[UserPage] [noargs constructor] [==============] After bindingContext with _viewmodel");
    }

    public UserPage(int userId)
    {
        Console.WriteLine("[UserPage] [constructor] [==============] Start with userId " + userId);
        Console.WriteLine("[UserPage] [constructor] [==============] Before InitializeComponent");
        InitializeComponent();
        Console.WriteLine("[UserPage] [constructor] [==============] After InitializeComponent");

        Console.WriteLine("[UserPage] [constructor] [==============] Before instantiation of UserViewModel");
        _viewModel = new UserViewModel();
        Console.WriteLine("[UserPage] [constructor] [==============] After instantiation of UserViewModel");
        Console.WriteLine("[UserPage] [constructor] [==============] Before call GetUser with id " + userId);
        _viewModel.GetUser(userId);
        Console.WriteLine("[UserPage] [constructor] [==============] After call GetUser with id " + userId);

        Console.WriteLine("[UserPage] [constructor] [==============] Before bindingContext with _viewmodel");
        BindingContext = _viewModel;
        Console.WriteLine("[UserPage] [constructor] [==============] After bindingContext with _viewmodel");
    }

    public LocalizationResourceManager LocalizationResourceManager
    => LocalizationResourceManager.Instance;

    private void OnLanguageSwitchToggled(object sender, ToggledEventArgs e)
    {

        Console.WriteLine("[UserPage] [OnLanguageSwitchToggled] [==============] object: " + sender  + "  met args " + e);
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ? 
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);
    }

    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[UserPage] [OnThemeSwitchToggled] [==============] object: " + sender + "  met args " + e);
        Console.WriteLine("[UserPage] [OnThemeSwitchToggled] [==============] Current theme: " + Application.Current.RequestedTheme );
        
        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            Console.WriteLine("[UserPage] [OnThemeSwitchToggled] [==============] Current theme: " + Application.Current.RequestedTheme + ". Switch to theme Light");
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Console.WriteLine("[UserPage] [OnThemeSwitchToggled] [==============] Current theme: " + Application.Current.RequestedTheme + ". Switch to theme Dark");
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        

    }

    private void OnEmailConfirmationSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[UserPage] [OnEmailConfirmationSwitchToggled] [==============] object: " + sender + "  met args " + e);
    }

    private async void OnEmailBtnClicked(object sender, EventArgs e)
    {
        if (Email.Default.IsComposeSupported)
        {
            var message = new EmailMessage
            {
                Subject = "Hi how are you?",
                Body = "Thanks for being here, nice to meet you!",
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(new[] { "aubreypolderman@gmail.com" })
            };
            message.Attachments.Add(new EmailAttachment(Path.Combine(FileSystem.CacheDirectory, "dotnet_bot.svg")));

        await Email.Default.ComposeAsync(message);
        }
    }
}