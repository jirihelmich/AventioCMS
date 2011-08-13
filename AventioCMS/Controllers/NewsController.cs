using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Subsystem;

namespace HTH8.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(long Id)
        {
            return View(_sl.GetSubsystem<NewsService, DomainModel.Entity.News>().GetById(Id));
        }

    }
}
