using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class WorkflowUnitOfWork: IDisposable
    {
        private EISTDbContext _context;
        private WorkflowRepository _workflowApprovalRepository;
        public WorkflowUnitOfWork(EISTDbContext context)
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
