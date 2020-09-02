using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FlyRemotely
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Catalog",
              url: "oferty/{categoryName}",
              defaults: new { controller = "Catalog", action = "Index" });

            routes.MapRoute(
              name: "Details",
              url: "oferty/szczegoly/{offerId}",
              defaults: new { controller = "Catalog", action = "Details" });

            routes.MapRoute(
               name: "Login",
               url: "moje-konto/zaloguj",
               defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
               name: "Register",
               url: "moje-konto/zarejestruj",
               defaults: new { controller = "Account", action = "Register" });

            routes.MapRoute(
               name: "UserData",
               url: "moje-konto/edycja-danych",
               defaults: new { controller = "Manage", action = "Index" });

            routes.MapRoute(
               name: "OffersList",
               url: "moje-konto/lista-ofert",
               defaults: new { controller = "Manage", action = "OffersList" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
