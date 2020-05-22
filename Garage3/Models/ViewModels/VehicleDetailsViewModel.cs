using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        public string UserFullName { get; set; }
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        [Display(Name = "Vehicle model")]
        public string VehicleModel { get; set; }
        public string Color { get; set; }
        public int Wheels { get; set; }
    }
}
