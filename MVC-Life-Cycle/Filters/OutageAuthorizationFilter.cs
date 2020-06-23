using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace MVC_Life_Cycle.Filters
{
    public class OutageAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public OutageAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var outageSwitch = _configuration.GetSection("FeatureSwitches")
                .GetChildren()
                .FirstOrDefault(o => o.Key == "Outage");
            
            if (outageSwitch != null && bool.Parse(outageSwitch.Value))
            {
                context.Result = new ViewResult // If this set this means no need to continue execution and instead return this. 
                {
                    ViewName = "Outage"
                };
            }
        }
    }
}