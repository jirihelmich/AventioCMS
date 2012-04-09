using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entity;
using HTH8.Controllers;
using Model.Subsystem;
using Model.ViewModel;

namespace HTH8.Areas.Admin.Controllers
{
    public class CategoryController : AdminController<CategoryService, Category>
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View(_sl.GetCategoriesList());
        }

        public override ActionResult Edit(long? id)
        {
            return View(_sl.GetCategoryEditModel(id));
        }

        public ActionResult SaveOrdering(OrderingModel model)
        {
            _sl.ReorderCategories(model);
            return RedirectToAction("Index");
        }

    }
}
