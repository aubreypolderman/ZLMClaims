using System.Globalization;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    public ClaimFormStep1Page(ClaimFormStep1ViewModel vm)

    {
        Console.WriteLine("[..............] [ClaimFormStep1Page] [ClaimFormStep1ViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }

}