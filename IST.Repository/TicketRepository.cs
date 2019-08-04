using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IST.Entities;

namespace IST.Repository
{
   public class TicketRepository:Repository<Ticket>
   {
       private ISTDbContext _context;

       public TicketRepository(ISTDbContext context) : base(context)
       {
           _context = context;
       }
        public bool IsTicketNameExist(string Name, string InitialName)
        {
            bool isNotExist = true;
            if (Name != string.Empty && InitialName == "undefined")
            {
                var isExist = _context.Tickets.Any(x => !x.IsDeleted && x.IssueName.ToLower().Equals(Name.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Name != string.Empty && InitialName != "undefined")
            {
                var isExist = _context.Tickets.Any(x => !x.IsDeleted && x.IssueName.ToLower() == Name.ToLower() && x.IssueName.ToLower() != InitialName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
        public virtual string GenerateTicketCode()
        {
            string fmt = "000.##";
            return string.Format("{0:yyMMdd}", DateTime.Now) +
                   (GetCount(DateTime.Now.Day) + 1).ToString(fmt);
        }
    }
}
