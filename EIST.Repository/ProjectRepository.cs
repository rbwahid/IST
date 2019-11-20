using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class ProjectRepository : Repository<Project>
    {
        public EISTDbContext _context;

        public ProjectRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }
        public bool IsCompanyProjectNameExist(string Name, string InitialName)
        {
            bool isNotExist = true;
            if (Name != string.Empty && InitialName == "undefined")
            {
                var isExist = _context.Projects.Any(x => !x.IsDeleted && x.Name.ToLower().Equals(Name.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Name != string.Empty && InitialName != "undefined")
            {
                var isExist = _context.Projects.Any(x => !x.IsDeleted && x.Name.ToLower() == Name.ToLower() && x.Name.ToLower() != InitialName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

        public IEnumerable<Project> GetAllProjectByCompanyId(int companyId)
        {
            return _context.Projects.Where(x => !x.IsDeleted && x.CompanyId == companyId);
        }
    }
}
