using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Filter
{
    public class IdRequiredFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if(!context.ActionArguments.TryGetValue("id", out object _))
            {
                context.Result = new NotFoundResult();
            }
        }

    }
}
