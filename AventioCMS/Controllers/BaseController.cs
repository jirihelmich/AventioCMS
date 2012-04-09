using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace HTH8.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Service layer (aka Model) instance.
        /// </summary>
        protected ServiceLayer _sl = new ServiceLayer();

        /// <summary>
        /// Loads a list of categories and stores it in the ViewBag.Categories property
        /// </summary>
        protected void _LoadCategories()
        {
            ViewBag.Categories = _sl.GetRootCategories();
        }


        /// <summary>
        /// Loads a list of pages and stores it in the ViewBag.Pages property
        /// </summary>
        protected void _LoadPages()
        {
            ViewBag.Pages = _sl.GetAllPages();
        }

        /// <summary>
        /// OnActionExecuting override. Loading of the layout data, extraction of the ActionDescriptor data.
        /// </summary>
        /// <param name="ctx"></param>
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            _LoadCategories();
            _LoadPages();

            ViewBag.RequestAction = ctx.ActionDescriptor.ActionName;
            ViewBag.RequestController = ctx.ActionDescriptor.ControllerDescriptor.ControllerName;
        }
    }
}
