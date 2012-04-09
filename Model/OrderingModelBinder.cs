using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Model.ViewModel;

namespace Model
{
    public class OrderingModelBinder: IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            OrderingModel model = new OrderingModel();
            model.Parent = new Dictionary<long, long?>();

            string pattern = @"Parent\[(\d+)\]";

            var paramCollection =
            controllerContext.RequestContext.HttpContext.Request.Params;

            foreach (string key in paramCollection.AllKeys)
            {
                Match m = Regex.Match(key, pattern);
                if (m.Success)
                {
                    try{
                        long value = long.Parse(paramCollection[key]);
                        model.Parent.Add(long.Parse(m.Groups[1].Value), value);
                    }catch(Exception e)
                    {
                        model.Parent.Add(long.Parse(m.Groups[1].Value), null);
                    }
                }
            }

            return model;
        }
    }
}
