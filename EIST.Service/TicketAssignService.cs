using EIST.Common;
using EIST.Entities;
using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Service
{
    public class TicketAssignService
    {
        private EISTDbContext _context;
        private TicketAssignUnitOfWork _ticketAssignUnitOfWork;
        private IssueService _issueService;
        public TicketAssignService()
        {
            _context = new EISTDbContext();
            _ticketAssignUnitOfWork = new TicketAssignUnitOfWork(_context);
            _issueService = new IssueService();
        }

        public IEnumerable<TicketAssign> GetAllTicketAssigns()
        {
            var TicketAssign = _ticketAssignUnitOfWork.TicketAssignRepository.GetAll();
            return TicketAssign.OrderBy(x => x.Id);
        }

        public TicketAssign GetTicketAssignById(int id)
        {
            return _ticketAssignUnitOfWork.TicketAssignRepository.GetById(id);
        }

        public int AddTicketAssign(TicketAssign ticketAssign)
        {
            var newTicketAssign = new TicketAssign
            {
                Status = ticketAssign.Status,
                IssueId = ticketAssign.IssueId,
                AssigneeId = ticketAssign.AssigneeId,
                Description = ticketAssign.Description,

                CreatedBy = ticketAssign.CreatedBy,
                CreatedAt = ticketAssign.CreatedAt,
                IsDeleted = ticketAssign.IsDeleted,
            };
            _ticketAssignUnitOfWork.TicketAssignRepository.Add(newTicketAssign);
            _ticketAssignUnitOfWork.Save();

            return newTicketAssign.Id;
        }

        public int GetAssignTicketCount()
        {
            return _ticketAssignUnitOfWork.TicketAssignRepository.GetCount(x => x.IssueId != null && x.Status == (byte)EnumIssueStatus.Accepted);
        }
        public int GetUnAssignTicketCount()
        {
            return _ticketAssignUnitOfWork.TicketAssignRepository.GetCount(x => x.IssueId == null && x.Status == (byte)EnumIssueStatus.Pending);
        }

        public int GetTicketCount()
        {
            return _ticketAssignUnitOfWork.TicketAssignRepository.GetCount();
        }

        public int EditTicketAssign(TicketAssign ticketAssign)
        {
            var ticketAssignEntry = GetTicketAssignById(ticketAssign.Id);
            if (ticketAssignEntry != null)
            {
                ticketAssignEntry.Description = ticketAssign.Description;
                //ticketAssignEntry.Status = ticketAssign.Status;
                ticketAssignEntry.IssueId = ticketAssign.IssueId;
                ticketAssignEntry.AssigneeId = ticketAssign.AssigneeId;

                ticketAssignEntry.UpdatedAt = ticketAssign.UpdatedAt;
                ticketAssignEntry.UpdatedBy = ticketAssign.UpdatedBy;
                _ticketAssignUnitOfWork.TicketAssignRepository.Update(ticketAssignEntry);
                _ticketAssignUnitOfWork.Save();
            }
            return ticketAssignEntry.Id;
        }

        public void DeleteTicketAssign(int id, string currUserId)
        {
            _ticketAssignUnitOfWork.TicketAssignRepository.Disable(id);
            _ticketAssignUnitOfWork.Save(currUserId);
        }
        public void UpdateTicketAssignStatus(int recordId, byte status)
        {
            var model = GetTicketByIssueId(recordId);
            if (model != null)
            {
                model.Status = status;
                //model.ApprovedDate = DateTime.Now;
                _ticketAssignUnitOfWork.Save();
                if (status == (byte)EnumTicketAssignStatus.Started)
                {
                    _issueService.UpdateTicketStatus(recordId, (byte)EnumIssueStatus.In_Progress);
                }
                else if (status == (byte)EnumTicketAssignStatus.Completed)
                {
                    _issueService.UpdateTicketStatus(recordId, (byte)EnumIssueStatus.Completed);
                }
            }
        }
        public TicketAssign GetTicketByIssueId(int issueId)
        {
            return _ticketAssignUnitOfWork.TicketAssignRepository.GetTicketByIssueId(issueId);
        }
        public void Dispose()
        {
            _ticketAssignUnitOfWork.Dispose();
        }
    }
}
