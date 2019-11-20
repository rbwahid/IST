using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class WorkflowRepository : Repository<Workflow>
    {
        private EISTDbContext _context;

        public WorkflowRepository(EISTDbContext context)
           : base(context)
        {
            _context = context;
        }
        public Workflow GetWorkflowByPosition(int recordId, int positionId)
        {
            return _context.Workflows.FirstOrDefault(x => !x.IsDeleted && x.RecordId == recordId && x.PositionId == positionId);
        }
        public IEnumerable<Workflow> GetWorkflowsByRecordId(int recordId, string formName)
        {
            return _context.Workflows.Where(x => !x.IsDeleted && x.RecordId == recordId && x.FormName == formName).ToList();
        }
    }
}
