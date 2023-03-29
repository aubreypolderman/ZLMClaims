using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllPhotosPage : ContentPage
{

    private readonly AllPhotosViewModel _viewModel;

    public AllPhotosPage()
	{
		InitializeComponent();
        _viewModel = new AllPhotosViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPostsAsync();
    }
}