using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkingPlaceListViewModel
    {
        public int ParkingSpot { get; set; }
        public DateTime Arrival { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string Username { get; set; }
        public string VehicleType { get; set; }
        public DateTime Duration { get; set; }
    }
}
