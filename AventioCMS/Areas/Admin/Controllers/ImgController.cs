using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entity;
using HTH8.Controllers;
using Model.Subsystem;
using Model.ViewResults;

namespace HTH8.Areas.Admin.Controllers
{
    public class ImgController : AdminController<ImageService, Image>
    {
        //
        // GET: /Admin/Image/

        [HttpPost]
        public ActionResult Upload()
        {
            Object info = _sl.SaveUploadedImage(Request);

            return new FileUploadJsonResult { Data = info };
        }

    }
}
