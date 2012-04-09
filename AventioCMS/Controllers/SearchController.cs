using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTH8.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/

        public ActionResult Index(string s)
        {
            return View(_sl.Search(s));
        }

    }
}
