using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Life_Cycle.Filters;
using MVC_Life_Cycle.Models;

namespace MVC_Life_Cycle.Controllers
{
    //[TypeFilter(typeof(OutageAuthorizationFilter))] // We can't simply use [OutageAuthorizationFilter] because OutageAuthorizationFilter contains constructor so TypeFilter take care of this.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("In Index Action");

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    _logger.LogInformation("The value of i is {LoopCountValue}", i);

                    if (i == 55)
                        throw new Exception("This is our demo Exception.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred in index action.");
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/contact-us", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}