using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class ParkingPlace
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "Parking start time")]
        public DateTime Arrival { get; set; } = DateTime.Now;

        //Foreign Key
        public int VehicleId { get; set; }

        //Navigation property
        public Vehicle Vehicle { get; set; }
    }
}