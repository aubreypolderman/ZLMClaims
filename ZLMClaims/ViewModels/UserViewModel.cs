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
    }

    public async Task LoadDataAsync()
    {
        // retrieve the userid from the preference set        
        int userId = Preferences.Default.Get("userId", -1);
        var user = await userService.GetUserByIdAsync(userId);
        // Assign the retrieved user data to the User property
        User = user; 
    }

    public async void OnThemeSwitchToggled()
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
    }

    public async void OnLanguageSwitchToggled()
    {
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);

    }

    public async void OnEmailConfirmationSwitchToggled()
    {
        if (switchToggleEmailConfirmation ? false: true);
    }
}
