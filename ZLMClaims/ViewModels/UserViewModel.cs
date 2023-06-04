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
        this.navigationService = navigationService;
        this.userService = userService;

       //  InitApp();
    }

    public async Task LoadDataAsync()
    {
        // retrieve the userid from the preference set        
        int userId = Preferences.Default.Get("userId", 1);
        Console.WriteLine(DateTime.Now + "[..............] [UserViewModel] [LoadDataAsync] retrieved user id: " + userId);
        var user = await userService.GetUserByIdAsync(userId);
        // Assign the retrieved user data to the User property
        User = user; 
        Console.WriteLine(DateTime.Now + "[..............] [UserViewModel] [LoadDataAsync] retrieved user name: " + user.Name);
        Console.WriteLine(DateTime.Now + "[..............] [UserViewModel] [LoadDataAsync] retrieved user email: " + user.Email);        
    }

    public void OnThemeSwitchToggled()
    {
        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            switchToggleDarkTheme = true;
        }

        // TODO Save to SQLite
    }

    public void OnLanguageSwitchToggled()
    {
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);

        // TODO Save to SQLite
    }

    public void OnEmailConfirmationSwitchToggled()
    {
        if (switchToggleEmailConfirmation ? false: true);
    }

    public async Task OnEmailBtnClicked()
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

    // Method for unit testing purpose only. The Title is not set in the actual app for some reason
    public void InitApp() 
    {
        Console.WriteLine(DateTime.Now + "[..............] [UserViewModel] [InitApp]");
        //this.Title = "User X Profile";
    }
}
