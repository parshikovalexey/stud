using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace studwebapimvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // отключаем возможность вывода данных в формате XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //добавил json
            config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}
