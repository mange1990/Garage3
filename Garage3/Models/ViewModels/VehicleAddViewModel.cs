using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleAddViewModel
    {
        [Required]
        [Range(0,40)]
        public int Wheels { get; set; }

        [Remote(action: "CheckRegNr", controller: "Vehicles")]
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [Display(Name ="Registration number")]
        public string RegistrationNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Manufacturer { get; set; }
        [Required]
        [StringLength(40)]
        public string Color { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "Vehicle model")]
        public string VehicleModel { get; set; }
        public int VehicleTypeId { get; set; }
    }
}
