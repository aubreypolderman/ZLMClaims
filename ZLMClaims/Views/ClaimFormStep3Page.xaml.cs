using System.Globalization;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep3Page : ContentPage
{

    public ClaimFormStep3Page(ClaimFormStep3ViewModel vm)

    {
        Console.WriteLine("[..............] [ClaimFormStep3Page] [ClaimFormStep3ViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("[..............] [ClaimFormStep3Page] [OnAppearing]");
    }
    
}