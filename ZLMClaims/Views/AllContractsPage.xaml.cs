using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllContractsPage : ContentPage
{
    private readonly AllContractsViewModel _viewModel;
    public AllContractsPage(AllContractsViewModel vm)
	{
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var contract = ((VisualElement)sender).BindingContext as Contract;
        if (contract == null)
        {
            return;
        }

        // Navigate to detail page of contract
        await Shell.Current.GoToAsync(nameof(ContractPage), true, new Dictionary<string, object>
        {
            {nameof(Contract), contract}
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetAllContracts();
    }
}