using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllUsersPage : ContentPage
{

    private readonly AllUsersViewModel _viewModel;

    public AllUsersPage()
	{
		InitializeComponent();
        _viewModel = new AllUsersViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPostsAsync();
    }
}