using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class CompanyUnitOfWork : IDisposable
    {

        private ISTDbContext _context { get; set; }
        private CompanyRepository _companyRepository { get; set; }

        public CompanyUnitOfWork(ISTDbContext context)
        {
            _context = context;
            _companyRepository = new CompanyRepository(_context);
        }
        public CompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository;
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
