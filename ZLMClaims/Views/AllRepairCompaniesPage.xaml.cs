using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllRepairCompaniesPage : ContentPage
{

    private readonly AllRepairCompaniesViewModel _viewModel;

    public AllRepairCompaniesPage()
	{
		InitializeComponent();
        _viewModel = new AllRepairCompaniesViewModel(this.Navigation);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }
}