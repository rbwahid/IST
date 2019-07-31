using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class CompanyProjectUnitOfWork : IDisposable
    {

        private ISTDbContext _context { get; set; }
        private CompanyProjectRepository _companyProjectRepository  { get; set; }

        public CompanyProjectUnitOfWork(ISTDbContext context)
        {
            _context = context;
            _companyProjectRepository = new CompanyProjectRepository(_context);
        }
        public CompanyProjectRepository CompanyProjectRepository
        {
            get
            {
                return _companyProjectRepository;
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
