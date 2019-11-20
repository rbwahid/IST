using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class IssueLabelUnitOfWork : IDisposable
    {
        public EISTDbContext _context;
        private IssueLabelRepository _issueLabelRepository;
        public IssueLabelUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _issueLabelRepository = new IssueLabelRepository(_context);
        }
        public IssueLabelRepository issueLabelRepository
        {
            get
            {
                return _issueLabelRepository;
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
