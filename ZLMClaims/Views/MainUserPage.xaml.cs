using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class MainUserPage : ContentPage
{

    private readonly MainViewModel _viewModel;

    public MainUserPage()
	{
		InitializeComponent();
        _viewModel = new MainViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPostsAsync();
    }
}