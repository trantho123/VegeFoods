using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VEGEFOODS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Shop",
                url: "Shop",
                defaults: new { controller = "ClientShop", action = "Shop", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Product",
               url: "Product",
               defaults: new { controller = "ClientProductSingle", action = "Product", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Cart",
               url: "Cart",
               defaults: new { controller = "ClientCart", action = "Cart", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Login",
              url: "Login",
              defaults: new { controller = "ClientLogin", action = "Login", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "SignUp",
              url: "SignUp",
              defaults: new { controller = "ClientLogin", action = "SignUp", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ClientHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
