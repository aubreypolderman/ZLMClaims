using System.Diagnostics;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class AllClaimsPage : ContentPage
{    
    private readonly AllClaimsViewModel _viewModel;
    public AllClaimsPage(AllClaimsViewModel vm)
	{
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var claimForm = ((VisualElement)sender).BindingContext as ClaimForm;
        if (claimForm == null)
        {
            return;
        }

        // Navigate to detail page of the claim
        Debug.WriteLine(DateTime.Now + "[.........][AllClaimsPage] TapGestureRecognizer_Tapped: navigate to claimForm with id " + claimForm.Id);
        await Shell.Current.GoToAsync(nameof(ClaimFormStep1Page), true, new Dictionary<string, object>
        {
            {nameof(ClaimForm), claimForm}
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // retrieve the userid from the SecureStorage 
        string userIdString = await SecureStorage.GetAsync("userId");
        int userId = userIdString != null ? int.Parse(userIdString) : -1;

        await _viewModel.GetAllClaims(userId);
    }
}