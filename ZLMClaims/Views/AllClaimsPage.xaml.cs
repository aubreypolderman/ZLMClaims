using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllClaimsPage : ContentPage
{
    
    private readonly AllClaimsViewModel _viewModel;
    public AllClaimsPage(AllClaimsViewModel vm)
	{
        Console.WriteLine("[..............] [AllClaimsPage] [AllContractsViewModel] viewmodel injected");
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Start");
        var claim = ((VisualElement)sender).BindingContext as Claim;
        if (claim == null)
        {
            Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Claim is null. Return");
            return;
        }
        Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Claim: " + claim);

        // Navigate to detail page of contract
        await Shell.Current.GoToAsync(nameof(ClaimFormStep1Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("[..............] [AllClaimsPage] [OnAppearing]");
        await _viewModel.GetAllClaims().ConfigureAwait(false);
    }
}