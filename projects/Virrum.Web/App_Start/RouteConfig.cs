using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Virrum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "QVC",
                url: "qvc/{action}/{*name}",
                defaults: new { controller = "Qvc", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Person", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
