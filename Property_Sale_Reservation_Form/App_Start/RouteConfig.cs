using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Property_Sale_Reservation_Form
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "LoadForm",
                url: "{controller}/{action}/{reference}",
                defaults: new { controller = "LoadForm", action = "Index", reference = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "ReturnPage",
                url: "{controller}/{action}",
                defaults: new { controller = "LoadForm", action = "ReturnPage" }
            );





        }
    }
}
