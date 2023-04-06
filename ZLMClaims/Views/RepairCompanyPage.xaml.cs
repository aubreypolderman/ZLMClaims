using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class RepairCompanyPage : ContentPage
{      
    private readonly RepairCompanyViewModel _viewModel;

    public RepairCompanyPage() 
    {
        Console.WriteLine("[RepairCompanyPage] [noargs constructor] [==============] Start");
        Console.WriteLine("[RepairCompanyPage] [noargs constructor] [==============] Before InitializeComponent");
        InitializeComponent();
        Console.WriteLine("[RepairCompanyPage] [noargs constructor] [==============] After InitializeComponent");
    }

}