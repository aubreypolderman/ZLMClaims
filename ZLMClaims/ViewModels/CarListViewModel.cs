using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        private readonly CarService carService;

        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel(CarService carService)
        {
            //Title = "My cars";
            this.carService = carService;        
        }

        [RelayCommand]
        async Task GetCarList() 
        {
           // if (IsLoading) return;
            try
            {
             //   IsLoading = true;    
                if (Cars.Any()) Cars.Clear();
                var cars = carService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to get cars", "OK");
            }
            finally
            { 
//IsLoading = false; 
            }
        }
    }
}
