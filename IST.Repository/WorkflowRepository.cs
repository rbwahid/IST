using IST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class WorkflowRepository : Repository<Workflow>
    {
        private ISTDbContext _context;

        public WorkflowRepository(ISTDbContext context)
           : base(context)
        {
            _context = context;
        }
        public Workflow GetWorkflowByPosition(int recordId, int positionId)
        {
            return _context.Workflows.FirstOrDefault(x => !x.IsDeleted && x.RecordId == recordId && x.PositionId == positionId);
        }
        public IEnumerable<Workflow> GetWorkflowsByRecordId(int recordId)
        {
            return _context.Workflows.Where(x => !x.IsDeleted && x.RecordId == recordId).ToList();
        }
    }
}
