using IST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class TicketAssignRepository : Repository<TicketAssign>
    {
        public ISTDbContext _context;

        public TicketAssignRepository(ISTDbContext context) : base(context)
        {
            _context = context;
        }
        public int GetCountByYear(int year)
        {
            return _context.TicketAssigns.Count(s => s.CreatedAt.Value.Year == year);
        }


    }
}
