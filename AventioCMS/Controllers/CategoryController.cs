using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTH8.Models.UrlModels;

namespace HTH8.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/Detail?Id=

        public ActionResult Detail(CategoryUrlModel model)
        {
            return View(_sl.GetCategory(model));
        }

    }
}
