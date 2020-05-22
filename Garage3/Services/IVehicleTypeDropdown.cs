using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Garage3.Services
{
    public interface IVehicleTypeDropdown
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}