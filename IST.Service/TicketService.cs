﻿using IST.Entities;
using IST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Service
{
    public class TicketService
    {
        private ISTDbContext _context;
        private TicketUnitOfWork _ticketUnitOfWork;
        private AttachmentFileUnitOfWork _attachmentFileUnitOfWork;
        public TicketService()
        {
            _context = new ISTDbContext();
            _ticketUnitOfWork = new TicketUnitOfWork(_context);
            _attachmentFileUnitOfWork = new AttachmentFileUnitOfWork(_context);
        }

        public IEnumerable<Ticket> GetAllTicket()
        {
            return _ticketUnitOfWork.TicketRepository.GetAll();
        }
        public Ticket GetTicketById(int id)
        {
            return _ticketUnitOfWork.TicketRepository.GetById(id);
        }

        public int AddTicket(Ticket Ticket)
        {
            var newTicket = new Ticket
            {
                Code = _ticketUnitOfWork.TicketRepository.GenerateTicketCode(),
                CompanyProjectId = Ticket.CompanyProjectId,
                IssueName = Ticket.IssueName,
                Description = Ticket.Description,
                Priority = Ticket.Priority,
                //AttachmentFileCollection = Ticket.AttachmentFileCollection,

                Status = Ticket.Status,
                CreatedAt = Ticket.CreatedAt,
                CreatedBy = Ticket.CreatedBy
            };
            _ticketUnitOfWork.TicketRepository.Add(newTicket);
            _ticketUnitOfWork.Save(Ticket.CreatedBy.ToString());
            return newTicket.Id;
        }
        public int EditTicket(Ticket Ticket)
        {
            var TicketEntry = GetTicketById(Ticket.Id);
            if (TicketEntry != null)
            {
                TicketEntry.CompanyProjectId = Ticket.CompanyProjectId;
                //TicketEntry.Code = Ticket.Code;
                TicketEntry.IssueName = Ticket.IssueName;
                TicketEntry.Description = Ticket.Description;
                TicketEntry.Priority = Ticket.Priority;

                //TicketEntry.Status = Ticket.Status;
                TicketEntry.UpdatedAt = Ticket.UpdatedAt;
                TicketEntry.UpdatedBy = Ticket.UpdatedBy;

                // Attachment File //
                //if (TicketEntry.AttachmentFileCollection.Any())
                //{
                //    foreach(var item in TicketEntry.AttachmentFileCollection)
                //    {
                //        _attachmentFileUnitOfWork.AttachmentFileRepository.DeleteFromDb(item);
                //        _attachmentFileUnitOfWork.Save();
                //    }
                //}
                _ticketUnitOfWork.TicketRepository.Update(TicketEntry);
                _ticketUnitOfWork.Save();
            }
            return TicketEntry.Id;
        }

        public void DeleteTicket(int id, string currUserId)
        {
            _ticketUnitOfWork.TicketRepository.Disable(id);
            _ticketUnitOfWork.Save(currUserId);
        }
        public bool IsTicketNameExist(string Name, string InitialName)
        {
            return _ticketUnitOfWork.TicketRepository.IsTicketNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _ticketUnitOfWork.Dispose();
        }

        public void AddAttachmentForTicket(List<AttachmentFile> attachmentList)
        {
            if(attachmentList != null)
            {
                foreach(var item in attachmentList)
                {
                    _attachmentFileUnitOfWork.AttachmentFileRepository.Add(item);
                    _attachmentFileUnitOfWork.Save();
                }
            }
        }
        public void Approve()
        {
            throw new NotImplementedException();
        }
        public void Disapprove()
        {
            throw new NotImplementedException();
        }

        public void UpdateTicketStatus(int recordId, byte status)
        {
            var model = GetTicketById(recordId);
            if(model != null)
            {
                model.Status = status;
                model.ApprovedDate = DateTime.Now;
                _ticketUnitOfWork.Save();
            }
        }
    }
}
