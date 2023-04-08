using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllClaimsPage : ContentPage
{

    private readonly AllClaimsViewModel _viewModel;

    public AllClaimsPage()
	{
		InitializeComponent();
        _viewModel = new AllClaimsViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        claimsCollection.SelectedItem = null;
    }
}