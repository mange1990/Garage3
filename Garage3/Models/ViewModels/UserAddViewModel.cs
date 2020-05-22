using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class UserAddViewModel
    {
        [Display(Name ="First name")]
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Range(0,200)]
        public int Age { get; set; }

        [Required]
        [StringLength(45)]
        [Remote(action: "CheckEmail", controller: "Users")]
        public string Email { get; set; }
    }
}
