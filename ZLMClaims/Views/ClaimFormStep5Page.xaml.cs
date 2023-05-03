using System.Globalization;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep5Page : ContentPage
{
    private readonly ClaimFormStep5ViewModel _viewModel;
    public ClaimFormStep5Page(ClaimFormStep5ViewModel vm)
	{
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
	}
    /*
    private void OnPrevBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep5Page] [OnPrevBtnClicked] [==============] sender => " + sender + " with args => " + e);
        Navigation.PopAsync();
    }

    private async void OnSendBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep5Page] [OnSendBtnClicked] [==============] sender => " + sender + " with args => " + e);
        await DisplayAlert("Confirmation", "The claim form has been succesfully sent to ZLM Verzekeringen", "OK");
        // Return to Home or Claims
        // Navigation.PushAsync(new AllClaimsPage());
    }
    */

}