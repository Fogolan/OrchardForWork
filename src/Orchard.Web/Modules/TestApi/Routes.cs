using System.Collections.Generic;
using System.Web.Http;
using Orchard.Mvc.Routes;

namespace TestApi
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new HttpRouteDescriptor {
                    Priority = 5,
                    RouteTemplate = "api/account/{action}/{id}",
                    Defaults = new {
                        area = "TestApi",
                        controller = "Account",
                        id = RouteParameter.Optional
                    }
                }
            };
        }
    }
}