using System;
using System.Collections.Generic;
using System.Web.Http;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace TestApi.Routing.Api
{
    public class ApiRoutes : IHttpRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]{
                new HttpRouteDescriptor {
                    Name = "Cart Api",
                    Priority = 1,
                    RouteTemplate = "api/cart/{id}",
                    Defaults = new {
                        area = "TestApi",
                        controller = "Cart",
                        id = RouteParameter.Optional
                    }
                },
                new HttpRouteDescriptor {
                    Name = "Account Api",
                    Priority = 2,
                    RouteTemplate = "api/account/",
                    Defaults = new {
                        area = "TestApi",
                        controller = "Account",
                    }
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }
    }
}