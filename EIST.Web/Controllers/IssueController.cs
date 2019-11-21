using EIST.Common;
using EIST.Web;
using EIST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Controllers
{
    [Authorize]
    //[CustomExceptionFilter]
    [Roles("Issue_Configuration", "Global_SupAdmin")]
    public class IssueController : Controller
    {
        public ActionResult Index()
        {
            return View(new IssueModel().GetAllTicket());
        }

        [Roles("Issue_Configuration", "Global_SupAdmin")]
        public ActionResult Add()
        {
            return View(new IssueModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IssueModel model)
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
            var model = new IssueModel(id);
            var authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            if((model.Status == (byte)EnumTicketStatus.Pending || model.Status == (byte)EnumTicketStatus.Rejected) && model.CreatedBy == authenticatedUserId)
            {
                return View("Edit", model);
            }
            return View("Details",model);
        }

        [Roles("Issue_Configuration", "Global_SupAdmin")]
        public ActionResult Edit(int id)
        {
            //var model = new TicketModel(id);
            //return View(model);
            return RedirectToAction("Details", "Issue", new { id = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IssueModel model)
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
            new IssueModel().DeleteTicket(id);
            return Json(new { meg = "success" });
        }
        #region json helper
        public JsonResult IsTicketNameExist(string IssueName, string InitialName)
        {
            bool isNotExist = new IssueModel().IsTicketNameExist(IssueName, InitialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region json helper
        public JsonResult RemoveAttachmentFileFromDbById(int fileId)
        {
            new IssueModel().RemoveAttachmentFileFromDbById(fileId);
            return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Approve
        public ActionResult Approve(WorkflowProcessModel workflowProcess)
        {
            new IssueModel().Approve(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Disapprove
        public ActionResult Disapprove(WorkflowProcessModel workflowProcess)
        {
            new IssueModel().Disapprove(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion

        public ActionResult TicketAssign(int id)
        {
            var model = new TicketAssignModel();
            model.IssueId = id;
            return PartialView("_TicketAssign", model);
        }
    }
}