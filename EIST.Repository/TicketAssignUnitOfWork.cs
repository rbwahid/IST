using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class TicketAssignUnitOfWork : IDisposable
    {

        private EISTDbContext _context { get; set; }
        private TicketAssignRepository _ticketAssignRepository  { get; set; }

        public TicketAssignUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _ticketAssignRepository = new TicketAssignRepository(_context);
        }
        public TicketAssignRepository TicketAssignRepository => _ticketAssignRepository;


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
