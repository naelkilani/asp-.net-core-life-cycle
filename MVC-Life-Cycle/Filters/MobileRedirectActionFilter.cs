using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_Life_Cycle.Filters
{
    public class MobileRedirectActionFilter : Attribute, IActionFilter
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.Keys.Contains("x-mobile"))
            {
                context.Result = new RedirectToActionResult(Action, Controller, null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action Executed");
        }
    }
}