using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HTH8.Utils.HtmlExtensions
{
    public static partial class HtmlExtensions
    {
        public static MvcHtmlString ActionLinkWithList(this HtmlHelper html,
                                                       string linkText,
                                                       string actionName,
                                                       string controllerName,
                                                       object routeValues)
        {
            UrlHelper h = new UrlHelper(html.ViewContext.RequestContext);

            RouteValueDictionary routeData = new RouteValueDictionary(routeValues);
            return new MvcHtmlString(
                String.Format(
                    "<a href='{0}'>{1}</a>",
                    h.Action(actionName, controllerName, FixListRouteDataValues(routeData)),
                    linkText
                    )
                );
        }

        public static MvcHtmlString ActionLinkWithList(this HtmlHelper html,
                                                       string linkText,
                                                       string actionName,
                                                       string controllerName,
                                                       object routeValues,
                                                       bool span)
        {
            UrlHelper h = new UrlHelper(html.ViewContext.RequestContext);

            RouteValueDictionary routeData = new RouteValueDictionary(routeValues);
            return new MvcHtmlString(
                String.Format(
                    "<a href='{0}'>{1}<span class='left'></span><span class='right'></span></a>",
                    h.Action(actionName, controllerName, FixListRouteDataValues(routeData)),
                    linkText
                    )
                );
        }

        public static MvcHtmlString ActionLinkWithList(this HtmlHelper html,
                                                       string linkText,
                                                       string actionName,
                                                       string controllerName,
                                                       object routeValues,
                                                       object htmlAttrs)
        {
            RouteValueDictionary routeData = new RouteValueDictionary(routeValues);
            return html.ActionLink(linkText, actionName, controllerName, FixListRouteDataValues(routeData), BuildAttrsDictionary(htmlAttrs));
        }

        internal static Dictionary<string, object> BuildAttrsDictionary(object o)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            if (o == null) return ret;

            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                ret[pi.Name] = pi.GetValue(o, null);
            }

            return ret;
        }

        internal static RouteValueDictionary FixListRouteDataValues(RouteValueDictionary routes)
        {
            var newRv = new RouteValueDictionary();
            foreach (var key in routes.Keys)
            {
                object value = routes[key];
                if (value is IEnumerable && !(value is string) && !(value is String))
                {
                    int index = 0;
                    foreach (string val in (IEnumerable)value)
                    {
                        newRv.Add(string.Format("{0}[{1}]", key, index), val);
                        index++;
                    }
                }
                else
                {
                    newRv.Add(key, value);
                }
            }

            return newRv;
        }
    }
}