using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkingplaceDeleteViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string VehicleRegistrationNumber { get; set; }

        public DateTime Arrival { get; set; }



    }
}
