using ZLMClaims.ViewModels;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;
using ZLMClaims.Models;

namespace ZLMClaims.Views;

public partial class ContractPage : ContentPage
{      
    private readonly ContractViewModel _viewModel;

    public ContractPage(ContractViewModel vm) 
    {
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

    private void OnButtonClaimClicked(object sender, EventArgs e)
    {
        //_viewModel.ClaimCommand.Execute(null);
        // Navigate to detail page of the claim
        var claimForm = new ClaimForm
        {
            ContractId = _viewModel.Contract.Id,
            DateOfOccurence = DateTime.Now,
            QCauseOfDamage = "cause",
            QWhatHappened = "test what happened",
            QWhereDamaged = "test where is the damage",
            QWhatIsDamaged = "test what is damaged",
            Image1 = "",
            Image2 = "",
            Street = "ChantalStreet",
            Suite = "123",
            City = "Rotterdam",
            ZipCode = "1234AS",
            Latitude = 0,
            Longitude = 0,
            Contract = _viewModel.Contract
        };
        Shell.Current.GoToAsync(nameof(ClaimFormStep1Page), true, new Dictionary<string, object>
        {
            {nameof(ClaimForm), claimForm}
        });
    }
}