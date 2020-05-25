using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkingPlaceListViewModel
    {
        public int Id { get; set; }
        public int ParkingSpot { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime Arrival { get; set; }

        [Display(Name = "Registration number")]
        public string VehicleRegistrationNumber { get; set; }

        [Display(Name = "Name")]
        public string Username { get; set; }

        [Display(Name = "Vehicle type")]
        public string VehicleTypeName { get; set; }
        public int VehicleType { get; set; }
        public string Duration => ((DateTime.Now - Arrival).ToString("dd'\\d 'hh'\\h 'mm\\m"));
    }
}
