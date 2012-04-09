using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Collections.Specialized;
using System.Reflection;

namespace HTH8.Utils.HtmlExtensions
{

    public static partial class HtmlExtensions
    {
        public static MvcHtmlString ActionLinkInheritData(this HtmlHelper html,
                                                       string linkText,
                                                       string actionName,
                                                       object routeValues)
        {
            RouteValueDictionary routeData = InheritData(html, routeValues);
            return html.ActionLink(linkText, actionName, routeData);
        }

        public static MvcHtmlString ActionLinkInheritData(this HtmlHelper html,
                                                       string linkText,
                                                       object routeValues)
        {
            RouteValueDictionary routeData = InheritData(html, routeValues);
            return html.ActionLink(linkText, html.ViewContext.RouteData.Values["action"].ToString(), routeData);
        }

        public static MvcHtmlString ActionLinkInheritData(this HtmlHelper html,
                                                       string linkText,
                                                       object routeValues,
                                                       object htmlAttrs)
        {
            RouteValueDictionary routeData = InheritData(html, routeValues);
            return html.ActionLink(linkText, html.ViewContext.RouteData.Values["action"].ToString(), routeData, htmlAttrs);
        }

        internal static RouteValueDictionary InheritData(HtmlHelper html, object routeValues)
        {
            RouteValueDictionary routeData = new RouteValueDictionary();

            ExtractValues(html.ViewContext.RouteData.Values, routeData);
            ExtractValues(html.ViewContext.HttpContext.Request.QueryString, routeData);
            ExtractValues(routeValues, routeData);
            return routeData;
        }

        private static void ExtractValues(RouteValueDictionary inheritedData, RouteValueDictionary routeData)
        {
            foreach (string name in inheritedData.Keys)
            {
                routeData[name] = inheritedData[name];
            }
        }

        private static void ExtractValues(NameValueCollection inheritedData, RouteValueDictionary routeData)
        {
            foreach (string name in inheritedData.Keys)
            {
                routeData[name] = inheritedData[name];
            }
        }

        private static void ExtractValues(object inheritedData, RouteValueDictionary routeData)
        {
            foreach (PropertyInfo pi in inheritedData.GetType().GetProperties())
            {
                routeData[pi.Name] = pi.GetValue(inheritedData, null);
            }
        }

    }
}