using IST.Common;
using IST.Entities;
using IST.Service;
using IST.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Models
{
    [NotMapped]
    public class TicketModel : Ticket
    {
        private string formName = "Ticket";
        private TicketService _ticketService;
        private AttachmentFileService _attachmentFileService;
        private CompanyProjectService _companyProjectService;
        private WorkflowService _workflowService;
        private UserService _userService;
        public List<AttachmentFileModel> FileLists { get; set; }
        public List<CompanyProject> CompanyProjectList { get; set; }
        int authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
        [Required]
        [Remote("IsTicketNameExist", "Ticket", AdditionalFields = "InitialName",
            ErrorMessage = "Issue Name already Exist")]
        [Display(Name = "Issue Name")]
        public new string IssueName
        {
            get { return base.IssueName; }
            set { base.IssueName = value; }
        }

        public TicketModel()
        {
            _ticketService = new TicketService();
            _attachmentFileService = new AttachmentFileService();
            _companyProjectService = new CompanyProjectService();
            _workflowService = new WorkflowService();
            _userService = new UserService();
            bool isCustomerUser = _userService.IsUserAsCustomer(authenticatedUserId,"Customer");
            if (isCustomerUser == true)
            {
                var userAsCustomer = _userService.GetCustomerByUserId(authenticatedUserId);
                CompanyProjectList = _companyProjectService.GetAllProjectByCompanyId(userAsCustomer.CompanyId.Value).ToList();
            }
            else
            {
                CompanyProjectList = _companyProjectService.GetAllCompanyProjects().ToList();
            }
        }

        public TicketModel(int id) : this()
        {
            var TicketEntry = _ticketService.GetTicketById(id);
            if (TicketEntry != null)
            {
                Id = TicketEntry.Id;
                Code = TicketEntry.Code;
                IssueName = TicketEntry.IssueName;
                Description = TicketEntry.Description;
                Priority = TicketEntry.Priority;
                CompanyProjectId = TicketEntry.CompanyProjectId;
                CompanyProject = TicketEntry.CompanyProject;
                AttachmentFileCollection = TicketEntry.AttachmentFileCollection;

                Status = TicketEntry.Status;
                CreatedAt = TicketEntry.CreatedAt;
                CreatedBy = TicketEntry.CreatedBy;
                CreatedByUser = TicketEntry.CreatedByUser;
                UpdatedAt = TicketEntry.UpdatedAt;
                UpdatedBy = TicketEntry.UpdatedBy;
                UpdatedByUser = TicketEntry.UpdatedByUser;
            }

        }
        public IEnumerable<Ticket> GetAllTicket()
        {
            return _ticketService.GetAllTicket();
        }
        public Ticket GetTicketById(int id)
        {
            return _ticketService.GetTicketById(id);
        }
        public int AddTicket()
        {
            base.Status = (byte)EnumTicketStatus.Pending;
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = authenticatedUserId;

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
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;

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
        public void RemoveAttachmentFileFromDbById(int fileId)
        {
            _attachmentFileService.RemoveAttachmentFileFromDbById(fileId);
        }
        public void Approve(WorkflowProcessModel workflowProcess)
        {
            var workflowModel = new WorkflowModel();
            workflowModel.FormName = formName;
            workflowModel.RecordId = workflowProcess.RecordId;
            workflowModel.ApproverId = workflowProcess.ApprovalId;
            workflowModel.Status = (byte)EnumTicketStatus.Accepted;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumTicketStatus), EnumTicketStatus.Accepted);
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
            workflowModel.Status = (byte)EnumTicketStatus.Rejected;
            workflowModel.ApprovalStatus = Enum.GetName(typeof(EnumTicketStatus), EnumTicketStatus.Rejected);
            workflowModel.Remarks = workflowProcess.ApprovalRemarks;
            _workflowService.AddWorkflow(workflowModel);
            _ticketService.UpdateTicketStatus(workflowModel.RecordId, workflowModel.Status);
        }
        public void Dispose()
        {
            _ticketService.Dispose();
        }
    }
}