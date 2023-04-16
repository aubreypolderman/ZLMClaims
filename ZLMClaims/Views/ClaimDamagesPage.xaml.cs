using ZLMClaims.ViewModels;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace ZLMClaims.Views;

public partial class ClaimDamagesPage : ContentPage
{      
    private readonly ClaimDamagesViewModel _viewModel;

    public ClaimDamagesPage(ClaimDamagesViewModel viewModel) 
    {
        Console.WriteLine("[..............] [ClaimDamagesPage] [noargs constructor] Start");
        InitializeComponent();

        BindingContext = viewModel;
    }
}