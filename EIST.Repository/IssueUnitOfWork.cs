using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class IssueUnitOfWork:IDisposable
    {
       private EISTDbContext _context { get; set; }
       private IssueRepository _ticketRepository { get; set; }

        public IssueUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _ticketRepository = new IssueRepository(_context);
        }

        public IssueRepository TicketRepository
        {
            get { return _ticketRepository; }
        }
        public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }
        public void Save()
        {
            _context.SaveChanges("");
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
