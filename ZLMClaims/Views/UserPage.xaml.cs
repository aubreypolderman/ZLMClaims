using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class UserPage : ContentPage
{      
    private readonly UserViewModel _viewModel;

    public UserPage() 
    {
        Console.WriteLine("[..............] [UserPage] [noargs constructor]");
        InitializeComponent();
       
        //_viewModel = new UserViewModel();
        //BindingContext = _viewModel;

        Console.WriteLine("[..............] [UserPage] [before LoadUserByIdAsync userid=2]");
        //_viewModel.LoadUserByIdAsync(1);
        Console.WriteLine("[..............] [UserPage] [after LoadUserByIdAsync userid=2]");
    }

    private void OnLanguageSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[..............] [UserPage] [OnLanguageSwitchToggled");
        _viewModel.OnLanguageSwitchToggled();
    }

    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine("[..............] [UserPage] [OnThemeSwitchToggled] Current theme: " + Application.Current.RequestedTheme );
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