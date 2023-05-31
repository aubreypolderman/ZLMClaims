using ZLMClaims.ViewModels;
using Microsoft.Maui.Controls;

namespace ZLMClaims.Views;

public partial class RepairCompanyPage : ContentPage
{      
    private readonly RepairCompanyViewModel _viewModel;

    public RepairCompanyPage(RepairCompanyViewModel viewModel) 
    {

        BindingContext = viewModel;
        InitializeComponent();
    }

    private void OnPhoneNumberTapped(object sender, EventArgs e)
    {
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