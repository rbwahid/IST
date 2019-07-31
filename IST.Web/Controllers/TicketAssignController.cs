using IST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Controllers
{
    [Authorize]
    [Roles("Global_SupAdmin,Configuration")]
    public class TicketAssignController : Controller
    {
        // GET: Product
        public ActionResult Index(TicketAssignModel model)
        {
            return View((new TicketAssignModel().GetAllTicketAssigns()));
        }

        public ActionResult Add()
        {
            return View(new TicketAssignModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TicketAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddTicketAssign();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public JsonResult GetTicketAssignDetailsById(int id)
        {
            var model = new TicketAssignModel(id);
            return Json(new { Id = model.Id, Remarks = model.Remarks, Description = model.Description, Code = model.Code , TicketId = model.TicketId  , UserId = model.UserId , Status = model.Status });
        }

        public ActionResult Details(int id)
        {
            return View(new TicketAssignModel(id));
        }

        public ActionResult Edit(int id)
        {
            return View(new TicketAssignModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditTicketAssign();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            new TicketAssignModel().DeleteTicketAssign(id);
            return Json(new { meg = "success" });

        }
    }
}