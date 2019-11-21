using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class IssueLabelRepository : Repository<IssueLabel>
    {
        private EISTDbContext _context;
        public IssueLabelRepository(EISTDbContext context):base(context)
        {
            _context = context;
        }
        public IEnumerable<IssueLabel> GetAllIssueLabel()
        {
            return _context.IssueLabels.Where(x => !x.IsDeleted).OrderBy(x => x.LabelTitle);
        }
        public bool IsIssueLabelExist(string LabelTitle, string InitialLabelTitle)
        {
            bool isNotExist = true;
            if (LabelTitle != string.Empty && InitialLabelTitle == "undefined")
            {
                var isExist = _context.IssueLabels.Any(x => !x.IsDeleted && x.LabelTitle.ToLower().Equals(LabelTitle.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (LabelTitle != string.Empty && InitialLabelTitle != "undefined")
            {
                var isExist = _context.IssueLabels.Any(x => !x.IsDeleted && x.LabelTitle.ToLower() == LabelTitle.ToLower() && x.LabelTitle.ToLower() != InitialLabelTitle.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
        public IssueLabel GetIssuelabelByName(string LabelTitle)
        {
            return _context.IssueLabels.FirstOrDefault(x => !x.IsDeleted && x.LabelTitle.ToUpper() == LabelTitle.ToUpper());
        }

        
    }
}
