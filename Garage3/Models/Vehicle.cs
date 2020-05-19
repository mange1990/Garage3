using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Wheel count")]
        [Required]
        [Range(0, 30)]
        public int Wheels { get; set; }

        [Display(Name = "Registration number")]
        [Required(ErrorMessage = "Enter the Vehicle RegNo!")]
        [StringLength(6, MinimumLength = 6)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(15)]
        public string Color { get; set; }

        [Display(Name = "Vehicle model")]
        [Required]
        [StringLength(15)]
        public string VehicleModel { get; set; }

        //Foreign keys
        public int VehicleTypeId { get; set; }
        public int UserId { get; set; }

        //Navigation properties
        public VehicleType VehicleType { get; set; }
        public User User { get; set; }
        public ParkingPlace ParkingPlace { get; set; }
    }
}
