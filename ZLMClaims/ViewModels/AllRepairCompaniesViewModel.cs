using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        readonly ObservableCollection<Position> _positions;

        public IEnumerable Positions => _positions;


        public ICommand SelectRepairCompanyCommand { get; }
        INavigationService navigationService;
        IRepairCompanyService repairCompanyService;
        IDialogService dialogService;

        public ObservableCollection<RepairCompany> RepairCompanies { get; private set; } = new();

        public AllRepairCompaniesViewModel(INavigationService navigationService, IRepairCompanyService repairCompanyService, IDialogService dialogService)
        {            
            this.navigationService = navigationService;
            this.repairCompanyService = repairCompanyService;
            this.dialogService = dialogService;

            _positions = new ObservableCollection<Position>()
        {
        };

        }

        public async Task GetAllRepairCompanies()
        {

            // if (IsLoading) return;
            try
            {
                //    IsBusy = true;
                if (RepairCompanies.Any()) RepairCompanies.Clear();

                var repaircompanies = await repairCompanyService.GetRepairCompaniesAsync();
                foreach (var repaircompany in repaircompanies)
                {
                    if (repaircompany != null)
                    { 
                        RepairCompanies.Add(repaircompany);
                        Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + repaircompany.Name);
                        
                        // calculate distance between current location and location of selected repaircompany
                        (double currentLatitude, double currentLongitude) = await GetCurrentLocation();
                        Console.WriteLine($"Latitude: {currentLatitude}, Longitude: {currentLongitude}");

                        string formattedDistance = CalculateDistanceInKm(currentLatitude, currentLongitude, repaircompany.Latitude, repaircompany.Longitude);

                        // Make a pin for every repaircompany
                        var position = new Position("distance: " + formattedDistance + " km", repaircompany.Name,  new Location(repaircompany.Latitude, repaircompany.Longitude));
                        _positions.Add(position);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesViewModel] [GetAllRepairCompanies] " + "ERROR message: " + ex.Message);
                Console.WriteLine($"Unable to get Repaircompanies: {ex.Message}");
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Repaircompanies", "OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }

        }

       
        public string CalculateDistanceInKm(double currentLongitude, double currentLatitude, double selectedLongitude, double selectedLatitude)
        {
            // calculate distance in kilometers between current location and location of selected repaircompany
            var currentLocation = new Location(currentLatitude, currentLongitude);
            var selectedLocation = new Location(selectedLatitude, selectedLongitude);
            double distance = Location.CalculateDistance(currentLocation, selectedLocation, DistanceUnits.Kilometers);
            string formattedDistance = distance.ToString("0.0");

            return formattedDistance;
        }

        public async Task<(double Latitude, double Longitude)> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    return (location.Latitude, location.Longitude);
                }
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine($"Unable to get location: {ex.Message}");
                await dialogService.DisplayAlertAsync("Error", "Failed to get current location", "OK");
            }
            finally
            {
                _isCheckingLocation = false;
            }

            // Return default values if location cannot be obtained
            return (0, 0);
        }


        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
