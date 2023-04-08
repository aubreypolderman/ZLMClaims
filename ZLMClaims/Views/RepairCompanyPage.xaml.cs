using ZLMClaims.ViewModels;
using Microsoft.Maui.Controls;

namespace ZLMClaims.Views;

public partial class RepairCompanyPage : ContentPage
{      
    private readonly RepairCompanyViewModel _viewModel;

    public RepairCompanyPage(RepairCompanyViewModel viewModel) 
    {
        Console.WriteLine("[..............] [RepairCompanyPage] [noargs constructor] Start");
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void OnPhoneNumberTapped(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [RepairCompanyPage] [OnPhoneNumberTapped] Start");
        //Device.OpenUri(new Uri("tel:1234567890"));
        /*
        var phoneDialer = DependencyService.Get<IPhoneDialer>();
        if (phoneDialer != null)
        {
           // bool canDial = await phoneDialer.DialAsync("1234567890");
            if (!canDial)
            {
                await DisplayAlert("Error", "Phone dialing not supported.", "OK");
            }
        }
        */
        if (PhoneDialer.Default.IsSupported)
            PhoneDialer.Default.Open("809-555-5454");

    }
}