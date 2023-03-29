using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllProductsPage : ContentPage
{

    private readonly AllProductsViewModel _viewModel;

    public AllProductsPage()
	{
		InitializeComponent();
        _viewModel = new AllProductsViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPostsAsync();
    }
}