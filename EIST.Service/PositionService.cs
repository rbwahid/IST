using EIST.Entities;
using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Service
{
    public class PositionService
    {
        private EISTDbContext _context;
        public PositionUnitOfWork _positionUnitOfWork;

        public PositionService()
        {
            _context = new EISTDbContext();
            _positionUnitOfWork = new PositionUnitOfWork(_context);
        }
        public Position GetPositionById(int id)
        {
            return _positionUnitOfWork.PositionRepository.GetById(id);
        }
        public Position GetPositionIdByName(string positionName)
        {
            return _positionUnitOfWork.PositionRepository.GetPositionByName(positionName);
        }
        public IEnumerable<Position> GetAllPositions()
        {
            return _positionUnitOfWork.PositionRepository.GetAllPositions();
        }
        public void AddPosition(Position position)
        {
            var newPosition = new Position
            {
                PositionName = position.PositionName,
                ShortName = position.ShortName,
                CreatedAt = position.CreatedAt,
                CreatedBy = position.CreatedBy
            };
            _positionUnitOfWork.PositionRepository.Add(newPosition);
            _positionUnitOfWork.Save();
        }
        public void EditPosition(Position position)
        {
            var positionEntry = GetPositionById(position.Id);
            if(positionEntry != null)
            {
                positionEntry.PositionName = position.PositionName;
                positionEntry.ShortName = position.ShortName;
                positionEntry.UpdatedAt = position.UpdatedAt;
                positionEntry.UpdatedBy = position.UpdatedBy;
                _positionUnitOfWork.Save();
            }
        }

        public void DeletePosition(int id,string currUserId)
        {
            _positionUnitOfWork.PositionRepository.Disable(id);
            _positionUnitOfWork.Save(currUserId);
        }
        public bool IsPositionNameExist(string PositionName, string InitialPositionName)
        {
            return _positionUnitOfWork.PositionRepository.IsPositionNameExist(PositionName, InitialPositionName);
        }
        public void Dispose()
        {
            _positionUnitOfWork.Dispose();
        }

        public bool CheckUserPosition(int userId, string positionName)
        {
            return _positionUnitOfWork.PositionRepository.CheckUserPosition(userId, positionName);
        }

        public bool CheckTicketProcessPosition(int userId)
        {
            return _positionUnitOfWork.PositionRepository.CheckTicketProcessPosition(userId);
        }
    }
}
