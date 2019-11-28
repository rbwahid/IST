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
    public class IssueService
    {
        private EISTDbContext _context;
        private IssueUnitOfWork _ticketUnitOfWork;
        private AttachmentFileUnitOfWork _attachmentFileUnitOfWork;
        private UserUnitOfWork _userUnitOfWork;
        public IssueService()
        {
            _context = new EISTDbContext();
            _ticketUnitOfWork = new IssueUnitOfWork(_context);
            _attachmentFileUnitOfWork = new AttachmentFileUnitOfWork(_context);
            _userUnitOfWork = new UserUnitOfWork(_context);

        }

        public IEnumerable<Issue> GetAllTicket()
        {
            return _ticketUnitOfWork.TicketRepository.GetAll();
        }
        public Issue GetTicketById(int id)
        {
            return _ticketUnitOfWork.TicketRepository.GetById(id);
        }

        public int AddTicket(Issue issue)
        {
            var newTicket = new Issue
            {
                Code = _ticketUnitOfWork.TicketRepository.GenerateTicketCode(),
                ProjectId = issue.ProjectId,
                IssueTitle = issue.IssueTitle,
                Description = issue.Description,
                
                Priority = issue.Priority,
                LabelId = issue.LabelId,
                Milestone = issue.Milestone,

                AttachmentFileCollection = issue.AttachmentFileCollection,

                Status = issue.Status,
                CreatedAt = issue.CreatedAt,
                CreatedBy = issue.CreatedBy
            };
            _ticketUnitOfWork.TicketRepository.Add(newTicket);
            _ticketUnitOfWork.Save(issue.CreatedBy.ToString());
            return newTicket.Id;
        }


        public int GetAvtiveIssueCount()
        {
            return _ticketUnitOfWork.TicketRepository.GetCount(x => x.IsDeleted == false && x.IsClosed == false && x.Status == (byte)EnumTicketStatus .Accepted);
        }
        public int GetCloseIssueCount()
        {
            return _ticketUnitOfWork.TicketRepository.GetCount(x=>x.IsDeleted==false && x.IsClosed==true);
        }

        public int GetIssueCount()
        {
          return  _ticketUnitOfWork.TicketRepository.GetCount();
        }


        public int EditTicket(Issue issue)

        {
            var IssueEntry = GetTicketById(issue.Id);
            if (IssueEntry != null)
            {
                IssueEntry.ProjectId = issue.ProjectId;                
                IssueEntry.IssueTitle = issue.IssueTitle;
                IssueEntry.Description = issue.Description;
                IssueEntry.Priority = issue.Priority;
                IssueEntry.LabelId = issue.LabelId;
                IssueEntry.Milestone = issue.Milestone;
                IssueEntry.Status = issue.Status;
                IssueEntry.UpdatedAt = issue.UpdatedAt;
                IssueEntry.UpdatedBy = issue.UpdatedBy;

                // Attachment File //
                //if (TicketEntry.AttachmentFileCollection.Any())
                //{
                //    foreach(var item in TicketEntry.AttachmentFileCollection)
                //    {
                //        _attachmentFileUnitOfWork.AttachmentFileRepository.DeleteFromDb(item);
                //        _attachmentFileUnitOfWork.Save();
                //    }
                //}
                _ticketUnitOfWork.TicketRepository.Update(IssueEntry);
                _ticketUnitOfWork.Save();
            }
            return IssueEntry.Id;
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

        public void TicketAssign(int issueId, ICollection<TicketAssign> ticketAssignCollection)
        {
            var IssueEntity = GetTicketById(issueId);
            if (IssueEntity != null)
            {
                IssueEntity.TicketAssignCollection = ticketAssignCollection;
                _ticketUnitOfWork.Save();
            }
        }
    }
}
