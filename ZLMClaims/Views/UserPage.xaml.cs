using System.Globalization;
using ZLMClaims.Resources.Languages;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class UserPage : ContentPage
{      
    private readonly UserViewModel _viewModel;

    public UserPage(UserViewModel vm) 
    {
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("[..............] [UserPage] [OnAppearing]");
        await _viewModel.LoadDataAsync();
    }

}