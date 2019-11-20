using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class PositionUnitOfWork:IDisposable
    {
        public EISTDbContext _context;
        private PositionRepository _positionRepository;
        public PositionUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _positionRepository = new PositionRepository(_context);
        }
        public PositionRepository PositionRepository
        {
            get
            {
                return _positionRepository;
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
