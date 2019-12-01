using EIST.Common;
using EIST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Controllers
{
    [Authorize]
    [Roles("Ticket_Assign_Configuration,Global_SupAdmin")]
    public class TicketAssignController : Controller
    {
        // GET: Product
        public ActionResult Index(TicketAssignModel model)
        {
            return View((new TicketAssignModel().GetAllTicketAssigns()));
        }
        public ActionResult CreateTicketAssign(int ticketId)
        {
            var model = new TicketAssignModel();
            var ticket = new TicketAssignModel().CreateTicketAssign(ticketId);
            model.Issue = ticket;
            if (ticket.TicketAssignCollection.Any())
            {
                return RedirectToAction("Details", "TicketAssign", new { id = ticket.TicketAssignCollection.FirstOrDefault().Id });
            }
            return View("Add",model);
        }
        public ActionResult Add()
        {
            var model = new TicketAssignModel();
            return View(model);
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
            return Json(new { Id = model.Id, Description = model.Description , TicketId = model.IssueId  , UserId = model.AssigneeId , Status = model.Status });
        }

        public ActionResult Details(int id)
        {
            var model = new TicketAssignModel(id);
            var authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            if ((model.Status == (byte)EnumTicketAssignStatus.Pending) && model.CreatedBy == authenticatedUserId)
            {
                return View("Edit", model);
            }
            return View("Details", model);
        }

        public ActionResult Edit(int id)
        {
            //return View(new TicketAssignModel(id));
            return RedirectToAction("Details", "TicketAssign", new { id = id });
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
        public JsonResult GetTicketsByProjectId(int projectId)
        {
            var project = new ProjectModel(projectId);
            return Json(project==null?null:project.TicketCollections.Select(s=>new {Id=s.Id , Name = s.IssueTitle }));

        }
        #region Approve
        public ActionResult Approve(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Approve(workflowProcess);
            return RedirectToAction("Details", "TicketAssign", new { id = workflowProcess.RecordId });
        }
        #endregion
        #region Disapprove
        public ActionResult Disapprove(WorkflowProcessModel workflowProcess)
        {
            new TicketAssignModel().Disapprove(workflowProcess);
            return RedirectToAction("Details", "TicketAssign", new { id = workflowProcess.RecordId });
        }
        #endregion
    }
}