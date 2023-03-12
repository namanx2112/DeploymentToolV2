using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DeploymentTool.Jwt.Filters;

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
        }
    }
}