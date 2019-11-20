using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class CompanyRepository : Repository<Company>
    {
        public EISTDbContext _context;

        public CompanyRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }
        public bool IsCompanyNameExist(string Name, string InitialName)
        {
            bool isNotExist = true;
            if (Name != string.Empty && InitialName == "undefined")
            {
                var isExist = _context.Companies.Any(x => !x.IsDeleted && x.Name.ToLower().Equals(Name.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Name != string.Empty && InitialName != "undefined")
            {
                var isExist = _context.Companies.Any(x => !x.IsDeleted && x.Name.ToLower() == Name.ToLower() && x.Name.ToLower() != InitialName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

    }
}
