using System.Globalization;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep5Page : ContentPage
{
    private readonly ClaimFormStep5ViewModel _viewModel;
    public ClaimFormStep5Page(ClaimFormStep5ViewModel vm)
	{
        _viewModel = vm;
        BindingContext = _viewModel;
        InitializeComponent();
    }

}