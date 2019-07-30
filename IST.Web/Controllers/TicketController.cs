using IST.Web;
using IST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Controllers
{
    [Authorize]
    //[CustomExceptionFilter]
    public class TicketController : Controller
    {
        [Roles("Ticket_Configuration", "Global_SupAdmin")]
        public ActionResult Index()
        {
            return View(new TicketModel().GetAllTicket());
        }

        [Roles("Ticket_Configuration", "Global_SupAdmin")]
        public ActionResult Add()
        {
            return View(new TicketModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddTicket();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new TicketModel(id);
            return View(model);
        }

        [Roles("Ticket_Configuration", "Global_SupAdmin")]
        public ActionResult Edit(int id)
        {
            var model = new TicketModel(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditTicket();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Roles("Ticket_Configuration", "Global_SupAdmin")]
        public ActionResult Delete(int id)
        {
            new TicketModel().DeleteTicket(id);
            return Json(new { meg = "success" });
        }
        #region json helper
        public JsonResult IsTicketNameExist(string IssueName, string InitialName)
        {
            bool isNotExist = new TicketModel().IsTicketNameExist(IssueName, InitialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region json helper
        public JsonResult RemoveAttachmentFileFromDbById(int fileId)
        {
            new TicketModel().RemoveAttachmentFileFromDbById(fileId);
            return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}