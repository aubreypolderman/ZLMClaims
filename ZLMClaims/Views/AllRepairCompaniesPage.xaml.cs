using Microsoft.Maui.Controls.Maps;
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

        var pin1 = new Pin
        {
            Label = "Van den Berg autoschade",
            Address = "Universeel schadeherstelbedrijf",
            Type = PinType.Place,
            Location = new Location(51.4616382, 3.5558194)
        };
        var pin2 = new Pin
        {
            Label = " Autoschade Sturm",
            Address = "Universeel schadeherstelbedrijf",
            Type = PinType.Place,
            Location = new Location(51.4625855, 3.5830258)
        };
        var pin3 = new Pin
        {
            Label = "Renova Goes",
            Address = "Merk schadeherstelbedrijf (BMW) ", 
            Type = PinType.Place,
            Location = new Location(51.4856149, 3.6026034)
        };
        map.Pins.Add(pin1);
        map.Pins.Add(pin2);
        map.Pins.Add(pin3);
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