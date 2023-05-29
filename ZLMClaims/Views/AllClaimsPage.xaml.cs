using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllClaimsPage : ContentPage
{
    
    private readonly AllClaimsViewModel _viewModel;
    public AllClaimsPage(AllClaimsViewModel vm)
	{
        Console.WriteLine(DateTime.Now + "[..............] [AllClaimsPage] [AllContractsViewModel] viewmodel injected");
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Start");
        var claim = ((VisualElement)sender).BindingContext as Claim;
        if (claim == null)
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Claim is null. Return");
            return;
        }
        Console.WriteLine(DateTime.Now + "[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Claim: " + claim);

        // Navigate to detail page of the claim
        await Shell.Current.GoToAsync(nameof(ClaimFormStep1Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine(DateTime.Now + "[..............] [AllClaimsPage] [OnAppearing]");
        await _viewModel.GetAllClaims().ConfigureAwait(false);
    }
}