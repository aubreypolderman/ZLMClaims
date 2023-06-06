using System.Globalization;
using ZLMClaims.Validations;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    public ValidatableObject<string> UserName { get; private set; }
    public ValidatableObject<string> Password { get; private set; }
    private readonly ClaimFormStep1ViewModel _viewModel;
    public ClaimFormStep1Page(ClaimFormStep1ViewModel vm)

    {
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

    private void AddValidations()
    {
        UserName.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "A username is required."
        });

        Password.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "A password is required."
        });
    }

}