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
        private ObservableCollection<RepairCompany> _repaircompanies;
        int _pinCreatedCount = 0;
        readonly ObservableCollection<Position> _positions;

        public IEnumerable Positions => _positions;


        public ICommand SelectRepairCompanyCommand { get; }
        INavigationService navigationService;
        IRepairCompanyService repairCompanyService;
        IDialogService dialogService;

        public ObservableCollection<RepairCompany> RepairCompanies { get; private set; } = new();
        public ObservableCollection<Pin> Pins { get; } = new ObservableCollection<Pin>();


        public AllRepairCompaniesViewModel(INavigationService navigationService, IRepairCompanyService repairCompanyService, IDialogService dialogService)
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [constructor] INavigation and IRepairCompanyService injected");            
            this.navigationService = navigationService;
            this.repairCompanyService = repairCompanyService;
            this.dialogService = dialogService;

            // GetAllRepairCompanies();
            _positions = new ObservableCollection<Position>()
        {
            new Position("New York, USA", "The City That Never Sleeps", new Location(40.67, -73.94)),
            new Position("Los Angeles, USA", "City of Angels", new Location(34.11, -118.41)),
            new Position("San Francisco, USA", "Bay City", new Location(37.77, -122.45))
        };

        }

        public async Task GetAllRepairCompanies()
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] Start");
            var page = Application.Current.MainPage.Navigation.NavigationStack.OfType<AllRepairCompaniesPage>().FirstOrDefault();
            var map = page?.FindByName<Microsoft.Maui.Controls.Maps.Map>("map");

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
                    // Calculate the distance between current location and location of repaircompany
                    // CalculateDistance(repaircompany.Address.Geo.Longitude, repaircompany.Address.Geo.Latitude);
                    // Console.WriteLine("[..............] [AllContractsViewModel] [GetAllRepairCompanies] Repaircompany:" + repaircompany.Name);
                    RepairCompanies.Add(repaircompany);
                    Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + repaircompany.Name);
                    var circle = new Circle
                    {
                        Center = new Location(51.89736284918831, 4.5121657098677925),
                        Radius = new Microsoft.Maui.Maps.Distance(250),
                        StrokeColor = Color.FromRgba("#0f92be"),
                        StrokeWidth = 8,
                        FillColor = Color.FromRgba("#cce5ed")
                    };

                    var pin = new Pin
                    {
                        Label = repaircompany.Name,
                        Address = repaircompany.Street,
                        Type = PinType.Place,
                        Location = new Location(repaircompany.Longitude, repaircompany.Latitude)
                    };
                    map.Pins.Add(pin);
                    map.MapElements.Add(circle);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Repaircompanies: {ex.Message}");
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Repaircompanies", "OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }

        }

        public double CalculateDistance(double longitude, double laitude)
        {
            Location current = new Location(42.358056, -71.063611);
            Location repaircompany = new Location(longitude, laitude);

            double miles = Location.CalculateDistance(current, repaircompany, DistanceUnits.Miles);
            return miles;


        }
    }
}
