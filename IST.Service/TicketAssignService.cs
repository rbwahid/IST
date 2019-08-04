﻿using IST.Entities;
using IST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Service
{
    public class TicketAssignService
    {
        private ISTDbContext _context;
        private TicketAssignUnitOfWork _ticketAssignUnitOfWork;

        public TicketAssignService()
        {
            _context = new ISTDbContext();
            _ticketAssignUnitOfWork = new TicketAssignUnitOfWork(_context);
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
                TicketId = ticketAssign.TicketId,
                UserId = ticketAssign.UserId,
                Description = ticketAssign.Description,

                CreatedBy = ticketAssign.CreatedBy,
                CreatedAt = ticketAssign.CreatedAt,
                IsDeleted = ticketAssign.IsDeleted,
            };
            _ticketAssignUnitOfWork.TicketAssignRepository.Add(newTicketAssign);
            _ticketAssignUnitOfWork.Save();

            return newTicketAssign.Id;
        }
        public int EditTicketAssign(TicketAssign ticketAssign)
        {
            var ticketAssignEntry = GetTicketAssignById(ticketAssign.Id);
            if (ticketAssignEntry != null)
            {
                ticketAssignEntry.Description = ticketAssign.Description;
                //ticketAssignEntry.Status = ticketAssign.Status;
                ticketAssignEntry.TicketId = ticketAssign.TicketId;
                ticketAssignEntry.UserId = ticketAssign.UserId;

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
            var model = GetTicketAssignById(recordId);
            if (model != null)
            {
                model.Status = status;
                model.ApprovedDate = DateTime.Now;
                _ticketAssignUnitOfWork.Save();
            }
        }
        public void Dispose()
        {
            _ticketAssignUnitOfWork.Dispose();
        }
    }
}
