using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZLMClaims.ViewModels;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using ZLMClaims.Models;
using ZLMClaims.Resources.Languages;
using ZLMClaims.Services;
public class UserViewModel : BaseViewModel
{
    // make the personid a global, sdo it could be used throughout the app. For now just hardcode it
    public int personid = 1;

    private User _user;
    INavigationService navigationService;
    IUserService userService;

    public User User
    {

        get { return _user; }
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public UserViewModel(INavigationService navigationService, IUserService userService)
    {
        Console.WriteLine("[..............] [UserViewModel] [constructor] Navigation and IUserService injected");
        this.navigationService = navigationService;
        this.userService = userService;

        // retrieve the customers id from the preference set
        var personId = Preferences.Get("personId", false);
        LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        Console.WriteLine("[..............] [UserViewModel] [LoadDataAsync]");

        var user = await userService.GetUserByIdAsync(personid);
        Console.WriteLine("[..............] [UserViewModel] [LoadDataAsync] retrieved user: " + user.Name);
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
        }
    }

    public void OnLanguageSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnLanguageSwitchToggled] ");
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);
    }

    public void OnEmailConfirmationSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnEmailConfirmationSwitchToggled]");
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
}
