using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace stud.webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/v1/{controller}/{action}/{id}",
               defaults: new { controller = "WeightingController", action = "GetWeightings", id = RouteParameter.Optional }
                );

        }
    }
}
