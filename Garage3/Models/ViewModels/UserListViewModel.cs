﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class UserListViewModel
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int VehicleCount { get; set; }
    }
}