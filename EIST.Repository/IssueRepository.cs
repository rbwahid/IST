using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIST.Entities;

namespace EIST.Repository
{
   public class IssueRepository:Repository<Issue>
   {
       private EISTDbContext _context;

       public IssueRepository(EISTDbContext context) : base(context)
       {
           _context = context;
       }
        public bool IsTicketNameExist(string Name, string InitialName)
        {
            bool isNotExist = true;
            if (Name != string.Empty && InitialName == "undefined")
            {
                var isExist = _context.Issues.Any(x => !x.IsDeleted && x.IssueTitle.ToLower().Equals(Name.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Name != string.Empty && InitialName != "undefined")
            {
                var isExist = _context.Issues.Any(x => !x.IsDeleted && x.IssueTitle.ToLower() == Name.ToLower() && x.IssueTitle.ToLower() != InitialName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
        
    }
}
