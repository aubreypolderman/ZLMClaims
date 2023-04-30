using System.Globalization;

namespace ZLMClaims.Views;

public partial class ClaimFormStep5Page : ContentPage
{
	public ClaimFormStep5Page()
	{
		InitializeComponent();
	}

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

}