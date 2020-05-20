using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleAddViewModel
    {
        public int Wheels { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Color { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleTypeId { get; set; }
    }
}
