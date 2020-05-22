using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Username { get; set; }
        [Display(Name = "Registration number")]
        public string VehicleRegistrationNumber { get; set; } //Vehicle

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "Parking time")]
        public DateTime Arrival { get; set; } //ParkingPlace


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "Check out time")]
        public DateTime CheckOut { get; set; }

        [DisplayFormat(DataFormatString = "{0} kr")]
        [Display(Name = "Total Price")]
        public int Price { get; set; }

        [DisplayFormat(DataFormatString = "{0} hours")]
        [Display(Name = "Total Time")]
        public int ParkingTime { get; set; }
    }
}
