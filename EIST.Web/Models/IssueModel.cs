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
    [NotMapped]
    public class TicketAssignSelectedModel
    {
        public int[] SelectedId { get; set; }
        public List<User> SelectedValueList { get; set; }
        public int IssueId { get; set; }
        public string TicketDescription { get; set; }
    }
    public class IssueModel : Issue
    {
        private string formName = "Issue";
        private IssueService _ticketService;
        private AttachmentFileService _attachmentFileService;
        private ProjectService _companyProjectService;
        private WorkflowService _workflowService;
        private UserService _userService;
        private IssueLabelService _issueLabelService;
        public List<AttachmentFileModel> FileLists { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<User> UserList { get; set; }
        public List<IssueLabel> IssueLabelList { get; set; }
        public TicketAssignSelectedModel TicketAssignSelectedModel { get; set; }

        int authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
        [Required]
        [Remote("IsTicketNameExist", "Issue", AdditionalFields = "InitialName",
            ErrorMessage = "Issue Name already Exist")]
        [Display(Name = "Issue Name")]
        public new string IssueTitle
        {
            get { return base.IssueTitle; }
            set { base.IssueTitle = value; }
        }
        
        public IssueModel()
        {
            _ticketService = new IssueService();
            _attachmentFileService = new AttachmentFileService();
            _companyProjectService = new ProjectService();
            _workflowService = new WorkflowService();
            _userService = new UserService();
            _issueLabelService = new IssueLabelService();
            
            bool isCustomerUser = _userService.IsUserAsCustomer(authenticatedUserId, EnumUserType.Customer.ToString());
            if (isCustomerUser == true)
            {
                var userAsCustomer = _userService.GetCustomerByUserId(authenticatedUserId);
                ProjectList = _companyProjectService.GetAllProjectByCompanyId(userAsCustomer.CompanyId.Value).ToList();
            }
            else
            {
                ProjectList = _companyProjectService.GetAllCompanyProjects().ToList();
            }

            IssueLabelList = _issueLabelService.GetAllIssueLabel().ToList();
            UserList = _userService.GetAllDeveloperRoleUser().ToList();

            TicketAssignSelectedModel = new TicketAssignSelectedModel();
            TicketAssignSelectedModel.SelectedValueList = _userService.GetAllDeveloperRoleUser().ToList();
        }

        public IssueModel(int id) : this()
        {
            var TicketEntry = _ticketService.GetTicketById(id);
            if (TicketEntry != null)
            {
                Id = TicketEntry.Id;
                Code = TicketEntry.Code;
                IssueTitle = TicketEntry.IssueTitle;
                Description = TicketEntry.Description;
                Priority = TicketEntry.Priority;
                ProjectId = TicketEntry.ProjectId;
                Project = TicketEntry.Project;
                IssueLabel = TicketEntry.IssueLabel;
                LabelId = TicketEntry.LabelId;
                Milestone = TicketEntry.Milestone;
                AttachmentFileCollection = TicketEntry.AttachmentFileCollection;
                TicketAssignCollection = TicketEntry.TicketAssignCollection;

                Status = TicketEntry.Status;
                CreatedAt = TicketEntry.CreatedAt;
                CreatedBy = TicketEntry.CreatedBy;
                CreatedByUser = TicketEntry.CreatedByUser;
                UpdatedAt = TicketEntry.UpdatedAt;
                UpdatedBy = TicketEntry.UpdatedBy;
                UpdatedByUser = TicketEntry.UpdatedByUser;
            }

        }

        public void TicketAssign(TicketAssignSelectedModel model)
        {
            // Multiple User Select (Ticket Assign) //
            List<TicketAssign> ticketAssignList = new List<TicketAssign>();
            if (model.SelectedId != null)
            {
                foreach (var assignSelectedId in model.SelectedId)
                {
                    var ticketAssign = new TicketAssign();
                    ticketAssign.IssueId = model.IssueId;
                    ticketAssign.AssigneeId = assignSelectedId;
                    ticketAssign.Description = model.TicketDescription;
                    ticketAssign.Status = (byte)EnumTicketAssignStatus.Pending;

                    ticketAssignList.Add(ticketAssign);
                }
                base.TicketAssignCollection = ticketAssignList;
                _ticketService.TicketAssign(model.IssueId, base.TicketAssignCollection);
            }
        }

        public IEnumerable<Issue> GetAllTicket()
        {
            return _ticketService.GetAllTicket();
        }
        public Issue GetTicketById(int id)
        {
            return _ticketService.GetTicketById(id);
        }
        public int AddTicket()
        {
            base.Status = (byte)EnumIssueStatus.Pending;
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = authenticatedUserId;

            //// Multiple User Select (Ticket Assign) //
            //List<TicketAssign> ticketAssignList = new List<TicketAssign>();
            //if (TicketAssignSelectedModel.SelectedId != null)
            //{
            //    foreach (var userId in TicketAssignSelectedModel.SelectedId)
            //    {
            //        var tickerAssign = new TicketAssign();
            //        tickerAssign.IssueId = base.Id;
            //        tickerAssign.AssigneeId = userId;
            //        tickerAssign.Description = base.Description;
            //        tickerAssign.Status = (byte)EnumTicketAssignStatus.Pending;

            //        ticketAssignList.Add(tickerAssign);
            //    }
            //    base.TicketAssignCollection = ticketAssignList;
            //}

            int ticketId = _ticketService.AddTicket(this);

            // Attachment File //
            List<AttachmentFile> attachmentList = new List<AttachmentFile>();
            
            if (FileLists != null)
            {
                foreach (var item in FileLists)
                {
                    if (item.FileBase != null)
                    {
                        var attachmentEntry = new AttachmentFileModel().SaveAttachmentFile(item, ticketId, authenticatedUserId);
                        attachmentList.Add(attachmentEntry);
                    }
                }
            }
            _ticketService.AddAttachmentForTicket(attachmentList);

            return ticketId;
        }
        public int EditTicket()
        {
            base.Status = (byte)EnumIssueStatus.Pending;
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = authenticatedUserId;

            int ticketId = _ticketService.EditTicket(this);
            // Attachment File //
            List<AttachmentFile> attachmentList = new List<AttachmentFile>();
            if (FileLists != null)
            {
                foreach (var item in FileLists)
                {
                    if (item.FileBase != null)
                    {
                        var attachmentEntry = new AttachmentFileModel().SaveAttachmentFile(item, ticketId, authenticatedUserId);
                        attachmentList.Add(attachmentEntry);
                    }
                }
            }
            _ticketService.AddAttachmentForTicket(attachmentList);

            return ticketId;
        }
        public void DeleteTicket(int id)
        {
            _ticketService.DeleteTicket(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }

        public bool IsTicketNameExist(string Name, string InitialName)
        {
            return _ticketService.IsTicketNameExist(Name, InitialName);
        }
        public void RemoveAttachmentFileById(int fileId)
        {
            _attachmentFileService.RemoveAttachmentFileFromDbById(fileId);
        }
        public void Approve(WorkflowProcessModel workflowProcess)
        {
            var workflowModel = new WorkflowModel();
            workflowModel.FormName = formName;
            workflowModel.RecordId = workflowProcess.RecordId;
            workflowModel.ApproverId = workflowProcess.ApprovalId;
            workflowModel.Status = (byte)EnumIssueStatus.Accepted;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumIssueStatus), EnumIssueStatus.Accepted);
            workflowModel.Remarks = workflowProcess.ApprovalRemarks;
            _workflowService.AddWorkflow(workflowModel);
            _ticketService.UpdateTicketStatus(workflowModel.RecordId, workflowModel.Status);
        }
        public void Disapprove(WorkflowProcessModel workflowProcess)
        {
            var workflowModel = new WorkflowModel();
            workflowModel.FormName = formName;
            workflowModel.RecordId = workflowProcess.RecordId;
            workflowModel.ApproverId = workflowProcess.ApprovalId;
            workflowModel.Status = (byte)EnumIssueStatus.Rejected;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumIssueStatus), EnumIssueStatus.Rejected);
            workflowModel.Remarks = workflowProcess.ApprovalRemarks;
            _workflowService.AddWorkflow(workflowModel);
            _ticketService.UpdateTicketStatus(workflowModel.RecordId, workflowModel.Status);
        }
        public void Dispose()
        {
            _ticketService.Dispose();
        }

        public List<Issue> GetAllTicketPagedList(IssueSearchModel model)
        {
           return _issueLabelService.GetAllTicketPagedList(model.SDateFrom,model.SDateTo,model.SCode,model.SIssueTitle,model.SProjectId);
        }
    }
}