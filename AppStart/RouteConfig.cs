using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;
using DeploymentTool.Jwt.Filters;
using Newtonsoft.Json.Converters;

namespace DeploymentTool.AppStart
{
    public class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
            config.Filters.Add(new JwtAuthenticationAttribute());

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            //Web API Exception Handler
            config.Filters.Add(new globalExceptionHandler());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
             new IsoDateTimeConverter());
        }
    }
}