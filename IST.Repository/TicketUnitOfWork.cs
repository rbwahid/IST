using IST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class TicketUnitOfWork:IDisposable
    {
       private ISTDbContext _context { get; set; }
       private TicketRepository _ticketRepository { get; set; }

        public TicketUnitOfWork(ISTDbContext context)
        {
            _context = context;
            _ticketRepository = new TicketRepository(_context);
        }

        public TicketRepository TicketRepository
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
