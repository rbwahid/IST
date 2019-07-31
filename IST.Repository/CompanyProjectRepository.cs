using IST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class CompanyProjectRepository : Repository<CompanyProject>
    {
        public ISTDbContext _context;

        public CompanyProjectRepository(ISTDbContext context) : base(context)
        {
            _context = context;
        }
        public bool IsCompanyProjectNameExist(string Name, string InitialName)
        {
            bool isNotExist = true;
            if (Name != string.Empty && InitialName == "undefined")
            {
                var isExist = _context.CompanyProjects.Any(x => !x.IsDeleted && x.Name.ToLower().Equals(Name.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Name != string.Empty && InitialName != "undefined")
            {
                var isExist = _context.CompanyProjects.Any(x => !x.IsDeleted && x.Name.ToLower() == Name.ToLower() && x.Name.ToLower() != InitialName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

    }
}
