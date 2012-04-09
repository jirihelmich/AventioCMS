using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entity;
using HTH8.Controllers;
using Model.Subsystem;

namespace HTH8.Areas.Admin.Controllers
{
    public class PageController : AdminController<PageService, Page>
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View(_sl.GetPagesList());
        }
    }
}
