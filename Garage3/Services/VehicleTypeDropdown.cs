using Garage3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Services
{
    public class VehicleTypeDropdown : IVehicleTypeDropdown
    {
        private readonly Garage3Context context;

        public VehicleTypeDropdown(Garage3Context context)
        {
            this.context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return context.VehicleTypes.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
        }
    }
}
