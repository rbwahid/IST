using EIST.Common;
using EIST.Web;
using EIST.Web.Models;
using PagedList;
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
        public ActionResult Index(IssueSearchModel model)
        {
            model.IssuePagedList = new IssueModel().GetAllTicketPagedList(model).ToPagedList(model.SPage, model.SPageSize);
            return View(model);
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
                var IssueId= model.AddTicket();
                return RedirectToAction("Details","Issue",new { id= IssueId });
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new IssueModel(id);
            //var authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            //if ((model.Status == (byte)EnumTicketStatus.Pending || model.Status == (byte)EnumTicketStatus.Rejected) && model.CreatedBy == authenticatedUserId)
            //{
            //    return View("Edit", model);
            //}
            return View(model);
        }

        [Roles("Issue_Configuration", "Global_SupAdmin")]
        public ActionResult Edit(int id)
        {
            //var model = new TicketModel(id);
            //return View(model);
            //return RedirectToAction("Details", "Issue", new { id = id });
            return View(new IssueModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IssueModel model)
        {
            if (ModelState.IsValid)
            {
                var IssueId = model.EditTicket();
                return RedirectToAction("Details", "Issue", new { id = IssueId });
            }
            return View(model);
        }

        [Roles("Issue_Configuration", "Global_SupAdmin")]
        public ActionResult Delete(int id)
        {
            new IssueModel().DeleteTicket(id);
            return Json(new { meg = "success" });
        }
        [Roles("Issue_Configuration", "Global_SupAdmin")]
        public ActionResult Design()
        {
            return View(new IssueModel());
        }
        [Roles("Issue_Configuration", "Global_SupAdmin")]
        [HttpPost]
        public ActionResult Design(IssueModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddTicket();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        #region json helper
        public JsonResult IsTicketNameExist(string IssueName, string InitialName)
        {
            bool isNotExist = new IssueModel().IsTicketNameExist(IssueName, InitialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region json helper
        public JsonResult RemoveAttachmentFileById(int id)
        {
            new IssueModel().RemoveAttachmentFileById(id);
            return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Issue Approve
        public ActionResult Approve(WorkflowProcessModel workflowProcess)
        {
            new IssueModel().Approve(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Issue Disapprove
        public ActionResult Disapprove(WorkflowProcessModel workflowProcess)
        {
            new IssueModel().Disapprove(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Issue Withhold
        public ActionResult Withhold(WorkflowProcessModel workflowProcess)
        {
            new IssueModel().Withhold(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Ticket Approve
        public ActionResult ApproveTicket(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Approve(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Ticket Disapprove
        public ActionResult DisapproveTicket(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Disapprove(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Ticket Delegate
        public ActionResult DelegateTicket(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Delegate(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Ticket Complete
        public ActionResult CompleteTicket(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Complete(workflowProcess);
            return RedirectToAction("Details", "Issue", new { id = workflowProcess.RecordId });
        }
        #endregion
        public ActionResult TicketAssign(IssueModel model)
        {
            new IssueModel().TicketAssign(model.TicketAssignSelectedModel);
            return RedirectToAction("Details", "Issue", new { id = model.TicketAssignSelectedModel.IssueId });
        }
        public JsonResult IssueClosed(int id)
        {
            new IssueModel().IssueClosed(id);
            return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestToIssueApproval(int id)
        { var mailUrlWithIssueId = Url.Action("Details", "Issue", new { id = id }, Request.Url.Scheme);
            bool isMailSend = false;
            var issueId = new IssueModel().RequestToIssueApproval(id, mailUrlWithIssueId, out isMailSend);
            if (isMailSend) { TempData["SuccessNotify"] = "Mail has been successfully send to Encoders."; }
            else { TempData["ErrorNotify"] = "Error occured, Unable to send mail to Encoders."; }
            return RedirectToAction("Details", "Issue", new { id = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IssueRequestApproval(int Id, int IssueAcceptRejectStatus, string Remarks)
        { var mailUrlWithIssueId = Url.Action("Details", "Issue", new { id = Id }, Request.Url.Scheme);
            bool isMailSend = false;
            var issueId = new IssueModel().IssueRequestApproval(Id, IssueAcceptRejectStatus, Remarks, mailUrlWithIssueId, out isMailSend);
            if (isMailSend) { TempData["SuccessNotify"] = "Mail has been successfully send to customer."; }
            else { TempData["ErrorNotify"] = "Error occured, Unable to send mail to customer."; }
            return RedirectToAction("Details", "Issue", new { id = issueId }); }
    }
}