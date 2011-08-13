using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Subsystem;
using HTH8.Utils.ImageResize.Models;
using HTH8.Utils.ImageResize.ActionResults;

namespace HTH8.Controllers
{
    public class ImageController : BaseController
    {
        //
        // GET: /Image/

        public ActionResult Thumbnail()
        {
            int width = Int32.Parse(Request.Params["w"]);
            int height = Int32.Parse(Request.Params["h"]);
            string path = Request.MapPath("~/files/" + Request.Params["path"]);

            ImageEditing resizer = new ImageEditing(path);
            return new ImageActionResult(resizer.Resize(width, height));
        }

        public ActionResult CarouselImage()
        {
            String title = _sl.GetSubsystem<CategoryService, DomainModel.Entity.Category>()
                .GetTitleByIDCulture((String)Request.Params["id"], Request.Params["culture"]);

            return new ImageActionResult(
                Utils.ImageCreator.CreateCarouselImage(title, Request.Params["number"])
            ); 
        }

    }
}
