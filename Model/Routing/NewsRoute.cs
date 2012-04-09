using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Model.Routing
{
    public class NewsRoute : Route, IRouteWithArea
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; private set; }

        //{culture}/{newskeyword}/{Id}-{Title}.html

        public NewsRoute(string area) : base(String.Empty, new MvcRouteHandler())
        {
            Area = area;
            DataTokens = new RouteValueDictionary();
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            string url = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2);
            var urlmatch = Regex.Match(url, @"(\w{2})/(novinky|news|nachrichten|новости)/(\d*)-([^.]*).html", RegexOptions.IgnoreCase);
            if (urlmatch.Success)
            {
                var routeData = new RouteData(this, this.RouteHandler);

                routeData.Values.Add("culture", urlmatch.Groups[1].Value);

                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(urlmatch.Groups[1].Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(urlmatch.Groups[1].Value);

                routeData.Values.Add("id", urlmatch.Groups[3].Value);
                routeData.Values.Add("title", urlmatch.Groups[4].Value);
                routeData.Values.Add("controller", this.Controller);
                routeData.Values.Add("action", this.Action);

                foreach (string key in DataTokens.Keys)
                {
                    routeData.Values.Add(key, DataTokens[key]);
                    routeData.DataTokens[key] = DataTokens[key];
                }

                return routeData;
            }
            else
                return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (values.ContainsKey("controller") && (!string.Equals(Controller, values["controller"] as string, StringComparison.InvariantCultureIgnoreCase)))
                return null;
            if (values.ContainsKey("action") && (!string.Equals(Action, values["action"] as string, StringComparison.InvariantCultureIgnoreCase)))
                return null;
            if ((!values.ContainsKey("Id")) || (!values.ContainsKey("Title")) || (!values.ContainsKey("news")) || (!values.ContainsKey("culture")))
                return null;
            return new VirtualPathData(this, string.Format("{0}/{1}/{2}-{3}.html", values["culture"], values["news"], values["id"], values["title"]));
        }
    }
}
