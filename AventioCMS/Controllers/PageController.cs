using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTH8.Controllers
{
    public class PageController : BaseController
    {
        //
        // GET: /Page/Detail?Id=

        public ActionResult Detail(long Id)
        {
            return View(_sl.GetSubsystem<Model.Subsystem.PageService, DomainModel.Entity.Page>().GetById(Id));
        }

    }
}
