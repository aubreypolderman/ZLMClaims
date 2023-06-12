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

    private async void OnLanguageSwitchToggled(object sender, ToggledEventArgs e)
    {
        _viewModel.OnLanguageSwitchToggled(); 
    }

    private async void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        _viewModel.OnThemeSwitchToggled();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }

}