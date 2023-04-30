using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllContractsPage : ContentPage
{
    private readonly AllContractsViewModel _viewModel;
    public AllContractsPage(AllContractsViewModel vm)
	{
        Console.WriteLine("[..............] [AllContractsPage] [AllContractsViewModel] viewmodel injected");
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Start");
        var contract = ((VisualElement)sender).BindingContext as Contract;
        if (contract == null)
        {
            Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Contract is null. Return");
            return;
        }
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Contract: " + contract);

        // Navigate to detail page of contract
        await Shell.Current.GoToAsync(nameof(ContractPage), true, new Dictionary<string, object>
        {
            {nameof(Contract), contract}
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("[..............] [AllContractsPage] [OnAppearing]");
        await _viewModel.GetAllContracts();
    }
}