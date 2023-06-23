using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep3Page : ContentPage
{
    private readonly ClaimFormStep3ViewModel _viewModel;
    public ClaimFormStep3Page(ClaimFormStep3ViewModel vm)

    {
        _viewModel = vm;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}