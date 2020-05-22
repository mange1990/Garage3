using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleTypeAddViewModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [Range(1,10)]
        public int Size { get; set; }
    }
}
