using EIST.Common;
using EIST.Entities;
using EIST.Service;
using EIST.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Models
{
    //[NotMapped]
    //public class UserSelectList
    //{
    //    public int[] SelectedId { get; set; }
    //    public List<User> SelectedValueList { get; set; }
    //}
    public class TicketAssignModel : TicketAssign
    {
        private string formName = "TicketAssign";
        //public UserSelectList UserSelectList { get; set; }
        private TicketAssignService _ticketAssignService;
        public UserService _userService;
        public IssueService _ticketService;
        public ProjectService _companyProjectService;
        private WorkflowService _workflowService;
        public IEnumerable<User> userList { get; set; }
        public IEnumerable<Issue> ticketList { get; set; }
        public IEnumerable<Project> companyProjectList { get; set; }

        public TicketAssignModel()
        {
            _ticketAssignService = new TicketAssignService();
            _userService = new UserService();
            _ticketService = new IssueService();
            _companyProjectService = new ProjectService();
            _workflowService = new WorkflowService();
            ticketList = _ticketService.GetAllTicket();
            userList = _userService.GetAllDeveloperRoleUser();
            companyProjectList = _companyProjectService.GetAllCompanyProjects();

            //UserSelectList = new UserSelectList();
            //UserSelectList.SelectedValueList = _userService.GetAllDeveloperRoleUser().ToList();
        }

        public TicketAssignModel(int id) : this()
        {
            var TicketAssignEntry = _ticketAssignService.GetTicketAssignById(id);
            if (TicketAssignEntry != null)
            {
                Id = TicketAssignEntry.Id;
                IssueId = TicketAssignEntry.IssueId;
                Description = TicketAssignEntry.Description;
                Status = TicketAssignEntry.Status;
                AssigneeId = TicketAssignEntry.AssigneeId;
                Issue = TicketAssignEntry.Issue;
                Assignee = TicketAssignEntry.Assignee;

                CreatedAt = TicketAssignEntry.CreatedAt;
                CreatedBy = TicketAssignEntry.CreatedBy;
                CreatedByUser = TicketAssignEntry.CreatedByUser;
                UpdatedAt = TicketAssignEntry.UpdatedAt;
                UpdatedBy = TicketAssignEntry.UpdatedBy;
                UpdatedByUser = TicketAssignEntry.UpdatedByUser;
            }
        }
        public Issue CreateTicketAssign(int ticketId)
        {
            var TicketEntry = _ticketService.GetTicketById(ticketId);
            return TicketEntry;
        }
        public IEnumerable<TicketAssign> GetAllTicketAssigns()
        {
            return _ticketAssignService.GetAllTicketAssigns();
        }

        public void AddTicketAssign()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.Status = (byte)EnumTicketAssignStatus.Pending;

            // Multiple User Select //
            //if(UserSelectList.SelectedId != null)
            //{
            //    foreach (var userId in UserSelectList.SelectedId)
            //    {
            //        base.AssigneeId = userId;
            //        _ticketAssignService.AddTicketAssign(this);
            //    }
            //} 
        }
        public void EditTicketAssign()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _ticketAssignService.EditTicketAssign(this);
        }
        public void DeleteTicketAssign(int id)
        {
            _ticketAssignService.DeleteTicketAssign(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }
        public void Approve(WorkflowProcessModel workflowProcess)
        {
            var workflowModel = new WorkflowModel();
            workflowModel.FormName = formName;
            workflowModel.RecordId = workflowProcess.RecordId;
            workflowModel.ApproverId = workflowProcess.ApprovalId;
            workflowModel.Status = (byte)EnumIssueStatus.Accepted;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumTicketAssignStatus), EnumTicketAssignStatus.Accepted);
            workflowModel.Remarks = workflowProcess.ApprovalRemarks;
            _workflowService.AddWorkflow(workflowModel);
            _ticketAssignService.UpdateTicketAssignStatus(workflowModel.RecordId, workflowModel.Status);
        }
        public void Disapprove(WorkflowProcessModel workflowProcess)
        {
            var workflowModel = new WorkflowModel();
            workflowModel.FormName = formName;
            workflowModel.RecordId = workflowProcess.RecordId;
            workflowModel.ApproverId = workflowProcess.ApprovalId;
            workflowModel.Status = (byte)EnumIssueStatus.Withhold;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumTicketAssignStatus), EnumTicketAssignStatus.Withhold);
            workflowModel.Remarks = workflowProcess.ApprovalRemarks;
            _workflowService.AddWorkflow(workflowModel);
            _ticketAssignService.UpdateTicketAssignStatus(workflowModel.RecordId, workflowModel.Status);
        }
        public void Dispose()
        {
            _ticketAssignService.Dispose();
        }
    }
}