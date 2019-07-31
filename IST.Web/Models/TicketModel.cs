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
        private TicketService _ticketService;
        private AttachmentFileService _attachmentFileService;
        public List<AttachmentFileModel> FileLists { get; set; }
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
        }

        public TicketModel(int id) : this()
        {
            var TicketEntry = _ticketService.GetTicketById(id);
            if (TicketEntry != null)
            {
                Id = TicketEntry.Id;
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
            base.Status = 1;
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = authenticatedUserId;
            
            int ticketId = _ticketService.AddTicket(this);
            // Attachment File //
            List<AttachmentFile> attachmentList = new List<AttachmentFile>();
            if (FileLists !=null)
            {
                foreach(var item in FileLists)
                {
                    if(item.FileBase != null)
                    {
                        var attachmentEntry = new AttachmentFileModel().SaveAttachmentFile(item,ticketId, authenticatedUserId);
                        attachmentList.Add(attachmentEntry);
                    }
                }
            }
            _ticketService.AddAttachmentForTicket(attachmentList);

            return ticketId;
        }
        public void EditTicket()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _ticketService.EditTicket(this);
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
        public void Dispose()
        {
            _ticketService.Dispose();
        }
    }
}