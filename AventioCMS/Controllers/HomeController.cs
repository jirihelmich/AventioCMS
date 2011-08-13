using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace HTH8.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {   
            return View(sl.GetFrontpage());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
