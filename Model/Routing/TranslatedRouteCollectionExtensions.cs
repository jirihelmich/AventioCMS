using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Routing
{
    public static class TranslatedRouteCollectionExtensions
    {
        public static TranslatedRoute MapTranslatedRoute(this RouteCollection routes, string name, string url, object defaults, object routeValueTranslationProviders, bool setDetectedCulture, string[] namespaces)
        {
            TranslatedRoute route = new TranslatedRoute(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(routeValueTranslationProviders),
                setDetectedCulture,
                new MvcRouteHandler());
            InitializeRouteParams(routes, name, namespaces, route);
            return route;
        }

        public static TranslatedRoute MapTranslatedRoute(this RouteCollection routes, string name, string url, object defaults, object routeValueTranslationProviders, object constraints, bool setDetectedCulture, string[] namespaces)
        {
            TranslatedRoute route = new TranslatedRoute(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(routeValueTranslationProviders),
                new RouteValueDictionary(constraints),
                setDetectedCulture,
                new MvcRouteHandler());
            InitializeRouteParams(routes, name, namespaces, route);
            return route;
        }

        private static void InitializeRouteParams(RouteCollection routes, string name, string[] namespaces,
                                                  TranslatedRoute route)
        {
            routes.Add(name, route);

            route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("RouteName", name);
            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens["Namespaces"] = namespaces;
            }
        }
    }
}
