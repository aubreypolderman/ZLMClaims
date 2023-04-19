using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class CarService : ICarService
    {
        public List<Car> GetCars()
        {
            return new List<Car>()
            {
                new Car
                {
                    Id = 1, Make = "KIA", Model = "Ceed 1.8", Vin = "1234"
                },
                new Car
                {
                    Id = 2, Make = "Opel", Model = "Astra 1.8", Vin = "5678"
                },
                new Car
                {
                    Id = 3, Make = "Peugot", Model = "508", Vin = "1359"
                },
            };
        }
    }
}
