using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MVC_Life_Cycle.Middlewares
{
    public class FeatureSwichAuthMidddleware
    {
        private readonly RequestDelegate _next;

        public FeatureSwichAuthMidddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            var routeAttribute = httpContext.GetEndpoint()?.Metadata.GetMetadata<RouteAttribute>();

            if (routeAttribute != null)
            {
                var featureSwitch = configuration.GetSection("FeatureSwitches")
                    .GetChildren()
                    .FirstOrDefault(o => o.Key == routeAttribute.Name);

                if (featureSwitch != null && !bool.Parse(featureSwitch.Value))
                {
                    httpContext.SetEndpoint(new Endpoint((context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return Task.CompletedTask;
                    }, 
                    EndpointMetadataCollection.Empty, "Feature Not Available"));
                }
            }

            await _next(httpContext);
        }
    }
}