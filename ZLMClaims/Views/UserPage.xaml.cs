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
        Console.WriteLine("[..............] [UserPage] [OnLanguageSwitchToggled");
        _viewModel.OnLanguageSwitchToggled();
    }

    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[..............] [UserPage] [OnThemeSwitchToggled] Current theme: " + Application.Current.RequestedTheme);
        _viewModel.OnThemeSwitchToggled();
    }

    private void OnEmailConfirmationSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[..............] [UserPage] [OnEmailConfirmationSwitchToggled]");
        _viewModel.OnEmailConfirmationSwitchToggled();
    }

    private async void OnEmailBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [UserPage] [OnEmailConfirmationSwitchToggled]");
        _viewModel?.OnEmailBtnClicked();
    }
}