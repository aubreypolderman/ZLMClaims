using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep3Page : ContentPage
{

    public ClaimFormStep3Page(ClaimFormStep3ViewModel vm)

    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep3Page] [ClaimFormStep3ViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep3Page] [OnAppearing]");
    }

    /*
    private void OnPrevBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep3Page] [OnPrevBtnClicked] [==============] sender => " + sender + " with args => " + e);
        Navigation.PopAsync();
    }
    private void OnNextBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep3Page] [OnNextBtnClicked] [==============] sender => " + sender + " with args => " + e);
        // Navigation.PushAsync(new ClaimFormStep4Page());
        var claim = ((VisualElement)sender).BindingContext as Claim;
        Shell.Current.GoToAsync(nameof(ClaimFormStep4Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });
    }
    */

}