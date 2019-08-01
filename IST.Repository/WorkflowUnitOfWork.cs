using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class WorkflowUnitOfWork: IDisposable
    {
        private ISTDbContext _context;
        private WorkflowRepository _workflowApprovalRepository;
        public WorkflowUnitOfWork(ISTDbContext context)
        {
            _context = context;
            _workflowApprovalRepository = new WorkflowRepository(_context);
        }
        public WorkflowRepository WorkflowRepository
        {
            get
            {
                return _workflowApprovalRepository;
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
