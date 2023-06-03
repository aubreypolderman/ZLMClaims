using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Contract), "Contract")]
    public partial class ContractViewModel : BaseViewModel   
    {
        private Contract _contract;
        private bool isUpdate = true;
        INavigationService navigationService;
        IUserService userService;
        public ContractViewModel(INavigationService navigationService, IUserService userService)
        {
            this.navigationService = navigationService;
            this.userService = userService;
        }

        [ObservableProperty]
        Contract contract;


        [RelayCommand]
        async Task Claim()
        {
            var claimForm = new ClaimForm();
            //var userObject = new User();
            //var userContract = new Contract();
            
            // Get user object
            //int userId = Preferences.Default.Get("userId", -1);
            //var user = await userService.GetUserByIdAsync(userId);
            // Assign the retrieved user data to the User property
            //userObject = user;
            //userContract = Contract;
            
            //claimForm.Contract = Contract; // Add Contract-object to ClaimForm
            claimForm.Contract = Contract;

            // Sla het ClaimForm-object op in de ClaimDataStorage samen met de informatie over de oorsprong
            ClaimDataStorage.ClaimForm = claimForm;
            ClaimDataStorage.IsUpdate = isUpdate;

            Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Contract id => " + Contract.Id);
            Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Licenseplate => " + Contract.LicensePlate);
            Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Product => " + Contract.Product);
            Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Make => " + Contract.Make);
            Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Model => " + Contract.Model);
            //Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] User id => " + Contract.User.Id);
            //Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Email => " + Contract.User.Email);
            //Console.WriteLine(DateTime.Now + "[..............] [ContractViewModel] [Claim] Street => " + Contract.User.Street);

            await navigationService.GoToAsync(nameof(ClaimFormStep1Page), true, new Dictionary<string, object>
            {
                { nameof(ClaimForm), claimForm }
            });
        }
        //async Task Claim() =>
        // await navigationService.GoToAsync(nameof(ClaimFormStep1Page));

    }

}
