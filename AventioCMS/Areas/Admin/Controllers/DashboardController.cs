using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entity;
using Model;
using Model.Subsystem;

namespace HTH8.Areas.Admin.Controllers
{
    public class DashboardController : AdminController<ProductService, Product>
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
