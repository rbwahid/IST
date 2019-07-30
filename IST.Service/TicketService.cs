using IST.Entities;
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
        public void EditTicket(Ticket Ticket)
        {
            var TicketEntry = GetTicketById(Ticket.Id);
            if (TicketEntry != null)
            {

                TicketEntry.Status = Ticket.Status;
                TicketEntry.UpdatedAt = Ticket.UpdatedAt;
                TicketEntry.UpdatedBy = Ticket.UpdatedBy;
                _ticketUnitOfWork.TicketRepository.Update(TicketEntry);
                _ticketUnitOfWork.Save();

            }
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
    }
}
