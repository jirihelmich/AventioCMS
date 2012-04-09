using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTH8.Models.UrlModels;
using Model.ViewModel;

namespace HTH8.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Detail(ProductUrlModel model)
        {
            return View(_sl.GetProductById(model));
        }

    }
}
