using System.Globalization;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    private readonly ClaimFormStep1ViewModel _viewModel;
    public ClaimFormStep1Page(ClaimFormStep1ViewModel vm)

    {
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

}