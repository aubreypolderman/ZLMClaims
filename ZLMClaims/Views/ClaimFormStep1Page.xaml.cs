using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    private readonly ClaimFormStep1ViewModel _viewModel;
    public ClaimFormStep1Page(ClaimFormStep1ViewModel vm)

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