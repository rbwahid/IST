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
        public IEnumerable<User> userList { get; set; }

        List<Ticket> ticketList = new List<Ticket>
        {
            new Ticket(){ Id=1,IsDeleted=false, IssueName="Issue 1",Description="no descp.!",Priority=true },
            new Ticket(){ Id=2,IsDeleted=false, IssueName="Issue 2",Description="no descp22.!",Priority=true },
            new Ticket(){ Id=3,IsDeleted=false, IssueName="Issue 3",Description="no descp33.!",Priority=false }
        };
        
        public TicketAssignModel()
        {
            _ticketAssignService = new TicketAssignService();
            _userService = new UserService();
            userList = _userService.GetAllUsers();
        }

        public TicketAssignModel(int id) : this()
        {
                var TicketAssignEntry = _ticketAssignService.GetTicketAssignById(id);
            if(TicketAssignEntry != null)
            {
                Id = TicketAssignEntry.Id;
                TicketId = TicketAssignEntry.TicketId;
                Description = TicketAssignEntry.Description;
                Remarks = TicketAssignEntry.Remarks;
                Code = TicketAssignEntry.Code;
                Status = TicketAssignEntry.Status;
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