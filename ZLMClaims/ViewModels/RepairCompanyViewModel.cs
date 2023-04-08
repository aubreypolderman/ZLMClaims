using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
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

    }

}
