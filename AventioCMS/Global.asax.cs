using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model;
using Model.Routing;
using Model.ViewModel;

namespace HTH8
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            CultureInfo cultureCS = CultureInfo.GetCultureInfo("cs-CZ");
            CultureInfo cultureEN = CultureInfo.GetCultureInfo("en-GB");
            CultureInfo cultureDE = CultureInfo.GetCultureInfo("de");
            CultureInfo cultureRU = CultureInfo.GetCultureInfo("ru");

            DictionaryRouteValueTranslationProvider translationProvider = new DictionaryRouteValueTranslationProvider(
                 new List<RouteValueTranslation>() {
                    new RouteValueTranslation(cultureCS, "News", "novinky"),
                    new RouteValueTranslation(cultureEN, "News", "news"),
                    new RouteValueTranslation(cultureDE, "News", "nachrichten"),
                    new RouteValueTranslation(cultureRU, "News", "новости")
                }
            );

            routes.MapRoute(
                "Images", // Route name
                "Image/{action}", // URL with parameters
                new { controller = "Image", action = "Thumbnail" }, // Parameter defaults
                new string[] { "HTH8.Controllers" }
            );

            NewsRoute n = new NewsRoute(String.Empty) { Controller = "News", Action = "Detail" };
            n.DataTokens["Namespaces"] = new string[] { "HTH8.Controllers" };
            routes.Add(n);

            PageRoute r = new PageRoute(String.Empty) { Controller = "Page", Action = "Detail" };
            r.DataTokens["Namespaces"] = new string[] { "HTH8.Controllers" };
            routes.Add(r);

            ProductRoute p = new ProductRoute(String.Empty) { Controller = "Product", Action = "Detail" };
            p.DataTokens["Namespaces"] = new string[] { "HTH8.Controllers" };
            routes.Add(p);

            CategoryRoute c = new CategoryRoute(String.Empty) { Controller = "Category", Action = "Detail" };
            c.DataTokens["Namespaces"] = new string[] { "HTH8.Controllers" };
            routes.Add(c);

            routes.MapTranslatedRoute(
                "Sitemap",
                "{culture}/{sitemap}.html",
                new { controller = "Map", action = "Index", id = "", culture = "cs" },
                new { controller = translationProvider, action = translationProvider },
                new { culture = @"cs|ru|de|en", sitemap = @"sitemap|mapa-webu|sitemap|kарта-сайта" },
                true,
                new string[] { "HTH8.Controllers" }
            );


            routes.MapTranslatedRoute(
                "TranslatedRoute",
                "{culture}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "", culture = "cs" },
                new { controller = translationProvider, action = translationProvider },
                new { culture = @"cs|ru|de|en" },
                true,
                new string[] { "HTH8.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[]{ "HTH8.Controllers" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders[typeof(OrderingModel)] =
                new OrderingModelBinder();

            ModelBinders.Binders[typeof(ProductOrderModel)] =
                new ProductOrderModelBinder();
        }
    }
}