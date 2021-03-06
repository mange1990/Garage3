﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        [Display(Name = "Vehicle count")]
        public int VehicleCount { get; set; }
    }
}
