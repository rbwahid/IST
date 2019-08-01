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
    public class TicketAssignModel : TicketAssign
    {
        private TicketAssignService _ticketAssignService;
        public UserService _userService;
        public TicketService _ticketService;
        public CompanyProjectService _companyProjectService;

        public IEnumerable<User> userList { get; set; }
        public IEnumerable<Ticket> ticketList { get; set; }
        public IEnumerable<CompanyProject> companyProjectList { get; set; }
      
        public TicketAssignModel()
        {
            _ticketAssignService = new TicketAssignService();
            _userService = new UserService();
            _ticketService = new TicketService();
            _companyProjectService = new CompanyProjectService();
            ticketList = _ticketService.GetAllTicket();
            userList = _userService.GetAllUsers();
            companyProjectList = _companyProjectService.GetAllCompanyProjects();
        }

        public TicketAssignModel(int id) : this()
        {
                var TicketAssignEntry = _ticketAssignService.GetTicketAssignById(id);
            if(TicketAssignEntry != null)
            {
                Id = TicketAssignEntry.Id;
                TicketId = TicketAssignEntry.TicketId;
                Description = TicketAssignEntry.Description;
               
                Code = TicketAssignEntry.Code;
                //Status = TicketAssignEntry.Status;
                UserId = TicketAssignEntry.UserId;
                Ticket = TicketAssignEntry.Ticket;
                User = TicketAssignEntry.User;
                

                CreatedAt = TicketAssignEntry.CreatedAt;
                CreatedBy = TicketAssignEntry.CreatedBy;
                CreatedByUser = TicketAssignEntry.CreatedByUser;
                UpdatedAt = TicketAssignEntry.UpdatedAt;
                UpdatedBy = TicketAssignEntry.UpdatedBy;
                UpdatedByUser = TicketAssignEntry.UpdatedByUser;
            } 

        }
        public IEnumerable<TicketAssign> GetAllTicketAssigns()
        {
            return _ticketAssignService.GetAllTicketAssigns();
        }
        
        public void AddTicketAssign()
        {
            
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.Status = (byte)EnumTicketAssignStatus.Draft;
            _ticketAssignService.AddTicketAssign(this);
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

        public void Dispose()
        {
            _ticketAssignService.Dispose();
        }
    }
}