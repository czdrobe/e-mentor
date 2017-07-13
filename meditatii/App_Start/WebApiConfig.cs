using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace meditatii.web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default_Api_WithApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Get" },
                constraints: new { controller = @"Api$" });

            config.Routes.MapHttpRoute(
                name: "Default_Api_WithApi_NoAction",
                routeTemplate: "api/{controller}",
                defaults: new { action = "Get" },
                constraints: new { controller = @"Api$" });

            config.Routes.MapHttpRoute(
                name: "Default_Api_Standard",
                routeTemplate: "api/{controller}Api/{action}",
                defaults: new { action = "Get" });

            config.EnsureInitialized();
        }
    }
}