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
        // GET: /Image/Thumbnail

        public ActionResult Thumbnail(int width, int height, string path)
        {
            string fullPath = Request.MapPath("~/files/" + path);

            ImageEditing resizer = new ImageEditing(fullPath);
            return new ImageActionResult(resizer.Resize(width, height));
        }

        //
        // GET: /Image/CarouselImage

        public ActionResult CarouselImage(string id, string culture, string number)
        {
            String title = _sl.GetSubsystem<CategoryService>()
                .GetTitleByIDCulture(id, culture);

            return new ImageActionResult(
                Utils.ImageCreator.CreateCarouselImage(title, number)
            ); 
        }

    }
}
