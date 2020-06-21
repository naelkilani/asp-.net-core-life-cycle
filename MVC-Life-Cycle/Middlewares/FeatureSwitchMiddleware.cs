using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MVC_Life_Cycle.Middlewares
{
    public class FeatureSwitchMiddleware
    {
        private readonly RequestDelegate _next;

        public FeatureSwitchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            if (!httpContext.Request.Path.Value.Contains("/features"))
                await _next(httpContext);
            else
            {
                var switches = configuration.GetSection("FeatureSwitches");
                var report = switches.GetChildren().Select(o => $"{o.Key} : {o.Value}");

                await httpContext.Response.WriteAsync(string.Join("\r\n", report));   
            }
        }
    }
}