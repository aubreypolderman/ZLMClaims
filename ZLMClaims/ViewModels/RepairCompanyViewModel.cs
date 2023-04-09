using CommunityToolkit.Mvvm.ComponentModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(RepairCompany), "RepairCompany")]
    public partial class RepairCompanyViewModel : ObservableObject   
    {
        private Models.RepairCompany _repaircompany;

        //public int Identifier => _repaircompany.Id;

        public RepairCompanyViewModel() { }

        [ObservableProperty]
        RepairCompany repairCompany;

        public void OnPhoneNumberTapped(string phone)
        {
            Console.WriteLine("[..............] [RepairCompanyViewModel] [OnPhoneNumberTapped]");
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open(phone);
        }

    }

}
