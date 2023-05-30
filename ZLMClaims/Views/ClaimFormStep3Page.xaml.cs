using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep3Page : ContentPage
{

    public ClaimFormStep3Page(ClaimFormStep3ViewModel vm)

    {
        BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}