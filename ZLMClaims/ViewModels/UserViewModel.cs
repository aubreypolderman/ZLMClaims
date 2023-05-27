using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZLMClaims.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ZLMClaims.Models;
using ZLMClaims.Resources.Languages;
using ZLMClaims.Services;
public partial class UserViewModel : BaseViewModel
{
    // make the personid a global, sdo it could be used throughout the app. For now just hardcode it

    public int personId; 

    public bool switchToggleDarkTheme = false;
    public bool switchToggleEmailConfirmation = false;

    [ObservableProperty]
    User user;

    INavigationService navigationService;
    IUserService userService;

    public UserViewModel(INavigationService navigationService, IUserService userService)
    {
        Console.WriteLine("[..............] [UserViewModel] [constructor] Navigation and IUserService injected");
        this.navigationService = navigationService;
        this.userService = userService;

       //  InitApp();
    }

    public async Task LoadDataAsync()
    {
        Console.WriteLine("[..............] [UserViewModel] [LoadDataAsync]");
        // retrieve the customers id from the preference set
        // var personId = Preferences.Get("personId", false);

        // becaUSE the above doens't work, let's just hardcode it for now
        personId = 1;

        var user = await userService.GetUserByIdAsync(personId);
        // Assign the retrieved user data to the User property
        User = user; 
        Console.WriteLine("[..............] [UserViewModel] [LoadDataAsync] retrieved user name: " + user.Name);
        Console.WriteLine("[..............] [UserViewModel] [LoadDataAsync] retrieved user email: " + user.Email);        
    }

    public void OnThemeSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Current theme: " + Application.Current.RequestedTheme);

        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Switch to theme Light");
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Switch to theme Dark");
            Application.Current.UserAppTheme = AppTheme.Dark;
            switchToggleDarkTheme = true;
        }

        // TODO Save to SQLite
    }

    public void OnLanguageSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnLanguageSwitchToggled] ");
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);

        // TODO Save to SQLite
    }

    public void OnEmailConfirmationSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnEmailConfirmationSwitchToggled]");
        if (switchToggleEmailConfirmation ? false: true);
    }

    public async Task OnEmailBtnClicked()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnEmailBtnClicked]");
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

    // Method for unit testing purpose only. The Title is not set in the actual app for some reason
    public void InitApp() 
    {
        Console.WriteLine("[..............] [UserViewModel] [InitApp]");
        //this.Title = "User X Profile";
    }
}
