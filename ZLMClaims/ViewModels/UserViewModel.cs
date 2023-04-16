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

public class UserViewModel : INotifyPropertyChanged
{
      
    private User _user;
    public User User
    {
        get
        {
            Debug.WriteLine("[UserViewModel] [Attribute User user] Getting user...");
            return _user;
        }
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /*
    private async Task LoadUsersAsync()
    {
        Users = await _userService.GetUsersAsync();
    }
    */

    public async Task LoadUserByIdAsync(int id)
    {
        Console.WriteLine("[..............] [UserViewModel] [LoadUserByIdAsync] userid = " + id);
        try
        {
            User = await _userService.GetUserByIdAsync(id);
        Console.WriteLine("[..............] [UserViewModel] [LoadUserByIdAsync] result user = " + User);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading user with id {id}: {ex.Message}");
        }
    }

    /* Waar is deze ook alweer voor bedoeld? */
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;

    protected virtual void OnPropertyChanged(string propertyName)
    {
    Console.WriteLine("[..............] [UserViewModel] [OnPropertyChanged] start with propertyname " + propertyName);
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
