using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string VehicleModel { get; set; }
        public string Color { get; set; }
        public int Wheels { get; set; }
    }
}
