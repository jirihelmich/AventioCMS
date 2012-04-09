using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel.Entity;
using HTH8.Controllers;
using HTH8.Utils;
using Model.Subsystem;
using Model.ViewModel;

namespace HTH8.Areas.Admin.Controllers
{
    public class ProductController : AdminController<ProductService, Product>
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index(long? categoryId)
        {
            return View(_sl.GetProductsList(categoryId));
        }

        public ActionResult Reorder (long categoryId, ProductOrderModel model)
        {
            _sl.ReorderProducts(categoryId, model);
            return RedirectToAction("Index", "Product", new {categoryId});
        }

        public override ActionResult Edit(long? id)
        {
            return View(_sl.GetProductEditModel(id));
        }

        public ActionResult ListDocuments(long? id)
        {
            return View(_sl.GetDocumentsModelByProductId(id));
        }

        [HttpPost]
        public ActionResult AddDocumentGroup(long id, FormCollection values)
        {
            _sl.SaveUploadedDocuments(Request, id, values);

            return RedirectToAction("ListDocuments", new {Id = id});
        } 

        public ActionResult DeleteGroup(long id, long productId)
        {
            _sl.DeleteDocumentsGroup(id);
            return RedirectToAction("ListDocuments", new { Id = productId });
        }
    }
}
