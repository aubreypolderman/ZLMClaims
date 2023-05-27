using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public class AllRepairCompaniesViewModel : BaseViewModel   
    {
       // private ObservableCollection<RepairCompany> _repaircompanies;
        readonly ObservableCollection<Position> _positions;

        public IEnumerable Positions => _positions;


        public ICommand SelectRepairCompanyCommand { get; }
        INavigationService navigationService;
        IRepairCompanyService repairCompanyService;
        IDialogService dialogService;

        public ObservableCollection<RepairCompany> RepairCompanies { get; private set; } = new();
        // public ObservableCollection<Pin> Pins { get; } = new ObservableCollection<Pin>();


        public AllRepairCompaniesViewModel(INavigationService navigationService, IRepairCompanyService repairCompanyService, IDialogService dialogService)
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [constructor] INavigation and IRepairCompanyService injected");            
            this.navigationService = navigationService;
            this.repairCompanyService = repairCompanyService;
            this.dialogService = dialogService;

            // GetAllRepairCompanies();
            _positions = new ObservableCollection<Position>()
        {
           // new Position("Rotterdam, Rotterdam Airportplein 60", "Van den Berg autoschade", new Location(51.95165042126455, 4.43385245511086)),
           // new Position("Rotterdam, Beverstraat 9C", "Van den Jagt autoschade", new Location(51.8974572425449, 4.511208184602901)),
           // new Position("Rotterdam, Hillevliet 44B", "Mossel autoschade", new Location(51.896881492621496, 4.504703826310183))
        };

        }

        public async Task GetAllRepairCompanies()
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] Start");

            // if (IsLoading) return;
            try
            {
                //    IsBusy = true;
                if (RepairCompanies.Any()) RepairCompanies.Clear();

                Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] Before invoke of RepairCompanyService");
                var repaircompanies = await repairCompanyService.GetRepairCompaniesAsync();
                Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] RepairCompanyService invoked");
                foreach (var repaircompany in repaircompanies)
                {
                    if (repaircompany != null)
                    { 
                        RepairCompanies.Add(repaircompany);
                        Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + repaircompany.Name);
                        var position = new Position(repaircompany.Name, repaircompany.Name, new Location(repaircompany.Latitude, repaircompany.Longitude));
                        _positions.Add(position);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + "ERROR message: " + ex.Message);
                Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + "ERROR: ex: " + ex);
                Debug.WriteLine($"Unable to get Repaircompanies: {ex.Message}");
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Repaircompanies", "OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }

        }

        /*
        public double CalculateDistance(double longitude, double laitude)
        {
            Location current = new Location(42.358056, -71.063611);
            Location repaircompany = new Location(longitude, laitude);

            double miles = Location.CalculateDistance(current, repaircompany, DistanceUnits.Miles);
            return miles;
        }
        */
    }
}
