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
    public class NewsController : AdminController<NewsService, News>
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View(_sl.GetNewsList());
        }

        public override ActionResult Edit(long? id)
        {
            return View(_sl.GetNewsEditModel(id));
        }

    }
}
