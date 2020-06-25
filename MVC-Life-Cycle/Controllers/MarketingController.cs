using Microsoft.AspNetCore.Mvc;
using MVC_Life_Cycle.Filters;
using SQLitePCL;

namespace MVC_Life_Cycle.Controllers
{
    public class MarketingController : Controller
    {
        [MobileRedirectActionFilter(Action = "NewSplash", Controller = "Marketing")]
        public IActionResult Splash()
        {
            return Content("The old splash page.");
        }

        public IActionResult NewSplash()
        {
            return Content("The new splash page.");
        }
    }
}