using IST.Entities;
using IST.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Models
{
    [NotMapped]
    public class PositionModel : Position
    {
        private PositionService _positionService;
        [Required]
        [Remote("IsPositionNameExist", "Position", AdditionalFields = "InitialName",
            ErrorMessage = "Project already Exist")]
        [Display(Name = "Position Name")]
        public new string PositionName
        {
            get { return base.PositionName; }
            set { base.PositionName = value; }
        }

        public PositionModel()
        {
            _positionService = new PositionService();
        }

        public PositionModel(int id) : this()
        {
            var positionEntry = _positionService.GetPositionById(id);
            if (positionEntry != null)
            {
                Id = positionEntry.Id;
                PositionName = positionEntry.PositionName;
                ShortName = positionEntry.ShortName;
                IsTicketProcess = positionEntry.IsTicketProcess;

                CreatedAt = positionEntry.CreatedAt;
                CreatedBy = positionEntry.CreatedBy;
                CreatedByUser = positionEntry.CreatedByUser;
                UpdatedAt = positionEntry.UpdatedAt;
                UpdatedBy = positionEntry.UpdatedBy;
                UpdatedByUser = positionEntry.UpdatedByUser;
            }

        }
        public IEnumerable<Position> GetAllPositions()
        {
            return _positionService.GetAllPositions();
        }

        public void AddPosition()
        {
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _positionService.AddPosition(this);
        }
        public void EditPosition()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _positionService.EditPosition(this);
        }
        public void DeletePosition(int id)
        {
            _positionService.DeletePosition(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }

        public bool IsPositionNameExist(string Name, string InitialName)
        {
            return _positionService.IsPositionNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _positionService.Dispose();
        }
    }
}