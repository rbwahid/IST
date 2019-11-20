using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class TicketAssignRepository : Repository<TicketAssign>
    {
        public EISTDbContext _context;

        public TicketAssignRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
