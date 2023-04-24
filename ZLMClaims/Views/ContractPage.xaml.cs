using ZLMClaims.ViewModels;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace ZLMClaims.Views;

public partial class ContractPage : ContentPage
{      
    private readonly ContractViewModel _viewModel;

    public ContractPage(ContractViewModel vm) 
    {
        Console.WriteLine("[..............] [ContractPage] [noargs constructor] Start");
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

    private void OnButtonClaimClicked(object sender, EventArgs e)
    {
        _viewModel.ClaimCommand.Execute(null);
    }
}