using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Model.ViewModel;

namespace Model
{
    public class ProductOrderModelBinder: IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ProductOrderModel model = new ProductOrderModel();
            
            List<long> list = new List<long>();
            string pattern = @"products\[\]";

            var paramCollection =
            controllerContext.RequestContext.HttpContext.Request.Params;

            for (int key = 0; key < paramCollection.AllKeys.Count(); ++key)
            {
                Match m = Regex.Match(paramCollection.AllKeys[key], pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    foreach (string val in paramCollection[key].Split(','))
                    {
                        long value = long.Parse(val);
                        list.Add(value);
                    }
                    break;
                }
            }

            model.Products = list.ToArray();

            return model;
        }
    }
}
