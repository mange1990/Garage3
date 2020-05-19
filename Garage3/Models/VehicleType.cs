using System.Collections.Generic;

namespace Garage3.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Size { get; set; }

        //Navigation property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}