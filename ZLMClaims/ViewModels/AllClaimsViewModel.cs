using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.ViewModels
{
    public partial class AllClaimsViewModel : BaseViewModel
    {
        public ObservableCollection<ClaimForm> _claimsForms;
        
        INavigationService navigationService;
        IClaimFormService claimFormService;
        IDialogService dialogService;
        IConnectivity connectivityService;

        // By making the claims observable, the view is automatically refreshed whenever a change occure
        public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

        public AllClaimsViewModel(INavigationService navigationService, IClaimFormService claimFormService, IDialogService dialogService, IConnectivity connectivityService)
        {
            this.navigationService = navigationService;
            this.claimFormService = claimFormService;
            this.dialogService = dialogService;
            this.connectivityService = connectivityService;
        }

        [RelayCommand]
        public async Task GetAllClaims()
        {

            // Get User by retrieving the userid from the SecureStorage 
            string userIdString = await SecureStorage.GetAsync("userId");
            int userId = userIdString != null ? int.Parse(userIdString) : -1;
            Debug.WriteLine(DateTime.Now + "[..........] [AllClaimsViewModel] [GetAllClaims] userId = " + userId );

            // Check internet connection
            NetworkAccess accessType = connectivityService.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                if (ClaimForms.Any()) ClaimForms.Clear();
                var claimForms = await claimFormService.GetAllClaimFormsByPersonIdAsync(userId);
                Debug.WriteLine(DateTime.Now + "[..........] [AllClaimsViewModel] [GetAllClaims] Nr. of claims received: " + claimForms.Count());

                foreach (var claimForm in claimForms)
                {
                    ClaimForms.Add(claimForm);
                }

            }
            else
            {
                Debug.WriteLine(DateTime.Now + "[..........] [AllClaimsViewModel] [GetAllClaims] No internet connection available");
                await dialogService.DisplayAlertAsync("Error", "Internet connection is NOT available!", "OK");
            }

        }

    }
}
