using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTH8.Controllers;
using Model.Subsystem;

namespace HTH8.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController<TS, TE> : BaseController
        where TS : AbstractService<TE>, new()
        where TE : DomainModel.Entity.EntityBase, new()
    {
        //
        // GET: /Admin/Admin/

        public ActionResult Delete(long id)
        {
            _sl.GetSubsystem<TS>().DeleteById(id);
            return RedirectToAction("Index");
        }

        public virtual ActionResult Edit(long? id)
        {
            return View(id == null ? new TE() : _sl.GetSubsystem<TS>().GetById((long)id));
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(long? id, FormCollection values)
        {
            _sl.GetSubsystem<TS>().Edit(id, values);
            return RedirectToAction("Index");
        }
    }
}
