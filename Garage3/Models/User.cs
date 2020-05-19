using System.Collections.Generic;

namespace Garage3.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public string Email { get; set; }

        //Navigation property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}