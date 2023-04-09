using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllContractsPage : ContentPage
{

    private readonly AllContractsViewModel _viewModel;

    public AllContractsPage()
	{
		InitializeComponent();
        _viewModel = new AllContractsViewModel(this.Navigation);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Start");
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] sender: " + sender);
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] arg: " + e);
        var contract = ((VisualElement)sender).BindingContext as Contract;
        if (contract == null)
        {
            Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Contract is null. Return");
            return;
        }
        Console.WriteLine("[..............] [AllContractsPage] [TapGestureRecognizer_Tapped] Contract: " + contract);

        // uncomment when detail contract page is there
        //await Shell.Current.GoToAsync(nameof(RepairCompanyPage), true, new Dictionary<string, object>
        //{
        //    {"Contract", contract}
        //});
    }
}