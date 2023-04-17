using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllClaimsPage : ContentPage
{

    public AllClaimsPage(AllClaimsViewModel vm)
    {
        Console.WriteLine("[..............] [AllClaimsPage] [AllClaimsViewModel] viewmodel injected");
        InitializeComponent();
        BindingContext = vm;
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        claimsCollection.SelectedItem = null;
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Start");
        var claim = ((VisualElement)sender).BindingContext as Claim;
        if (claim == null)
        {
            Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Contract is null. Return");
            return;
        }
        Console.WriteLine("[..............] [AllClaimsPage] [TapGestureRecognizer_Tapped] Contract: " + claim);

        // Navigate to detail page of contract
        await Shell.Current.GoToAsync(nameof(AllClaimsPage), true, new Dictionary<string, object>
        {
            {"Claim", claim}
        });
    }
}