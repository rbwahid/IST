using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class CustomerUserProjectRepository : BaseRepository<CustomerUserProject>
    {
        public EISTDbContext _context;

        public CustomerUserProjectRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }

        public List<CustomerUserProject> GetAllByProjectId(int id)
        {
            return _context.CustomerUserProjects.Where(s => s.ProjectId == id).ToList();
        }


    }
}
