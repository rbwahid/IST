using IST.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Repository
{
    public class PositionRepository:Repository<Position>
    {
        public ISTDbContext _context;
        public PositionRepository(ISTDbContext context)
            : base(context)
        {
            _context = context;
        }
        public IEnumerable<Position> GetAllPositions()
        {
            return _context.Positions.Where(x => !x.IsDeleted).OrderBy(x => x.PositionName); 
        }
        public bool IsPositionNameExist(string PositionName, string InitialPositionName)
        {
            bool isNotExist = true;
            if (PositionName != string.Empty && InitialPositionName == "undefined")
            {
                var isExist = _context.Positions.Any(x => !x.IsDeleted && x.PositionName.ToLower().Equals(PositionName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (PositionName != string.Empty && InitialPositionName != "undefined")
            {
                var isExist = _context.Positions.Any(x => !x.IsDeleted && x.PositionName.ToLower() == PositionName.ToLower() && x.PositionName.ToLower() != InitialPositionName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
        public Position GetPositionByName(string positionName)
        {
            return _context.Positions.FirstOrDefault(x => !x.IsDeleted && x.PositionName.ToUpper() == positionName.ToUpper());
        }

        public bool CheckUserPosition(int userId, string positionName)
        {
            return _context.Positions.Any(u => u.Id == userId && u.PositionName == positionName && !u.IsDeleted);
        }

        public bool CheckTicketProcessPosition(int userId)
        {
            return _context.Positions.Any(u => u.UserCollection.Any(x => x.Id == userId) && u.IsTicketProcess == true && !u.IsDeleted); ;
        }
    }
}
