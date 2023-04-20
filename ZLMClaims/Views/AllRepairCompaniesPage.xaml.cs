using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllRepairCompaniesPage : ContentPage
{

    private readonly AllRepairCompaniesViewModel _viewModel;

    public AllRepairCompaniesPage(AllRepairCompaniesViewModel vm)
	{
        Console.WriteLine("[..............] [AllRepairCompaniesPage] [AllRepairCompaniesViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Console.WriteLine("[AllRepairCompaniesPage] [TapGestureRecognizer_Tapped] [==============] Start");
        Console.WriteLine("[AllRepairCompaniesPage] [TapGestureRecognizer_Tapped] [==============] sender: "+ sender);
        Console.WriteLine("[AllRepairCompaniesPage] [TapGestureRecognizer_Tapped] [==============] arg: " + e);
        var repairCompany = ((VisualElement)sender).BindingContext as RepairCompany;
        if (repairCompany == null)
        {
            Console.WriteLine("[AllRepairCompaniesPage] [TapGestureRecognizer_Tapped] [==============] Company is null. Return");
            return;
        }
        Console.WriteLine("[AllRepairCompaniesPage] [TapGestureRecognizer_Tapped] [==============] Company is not null");

        await Shell.Current.GoToAsync(nameof(RepairCompanyPage), true, new Dictionary<string, object>
        {
            {nameof(RepairCompany), repairCompany}
        });
    }
}