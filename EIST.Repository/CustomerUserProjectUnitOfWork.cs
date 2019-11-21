using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class CustomerUserProjectUnitOfWork : IDisposable
    {

        private EISTDbContext _context { get; set; }
        private CustomerUserProjectRepository _customerUserProjectRepository { get; set; }

        public CustomerUserProjectUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _customerUserProjectRepository = new CustomerUserProjectRepository(_context);
        }
        public CustomerUserProjectRepository CustomerUserProjectRepository
        {
            get
            {
                return _customerUserProjectRepository;
            }
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
