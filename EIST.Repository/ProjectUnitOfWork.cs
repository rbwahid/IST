using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class ProjectUnitOfWork : IDisposable
    {

        private EISTDbContext _context { get; set; }
        private ProjectRepository _companyProjectRepository  { get; set; }

        public ProjectUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _companyProjectRepository = new ProjectRepository(_context);
        }
        public ProjectRepository CompanyProjectRepository
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
