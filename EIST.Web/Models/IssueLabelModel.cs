using EIST.Entities;
using EIST.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace EIST.Web.Models
{
    [NotMapped]
    public class IssueLabelModel : IssueLabel
    {
        private IssueLabelService _issueLabelService;
        [Required]
        [Remote("IsIssueLabelExist", "IssueLabel", AdditionalFields = "InitialLabelTitle",
            ErrorMessage = "Project already Exist")]
        [Display(Name = "Label Title")]
        public new string LabelTitle
        {
            get { return base.LabelTitle; }
            set { base.LabelTitle = value; }
        }

        public IssueLabelModel()
        {
            _issueLabelService = new IssueLabelService();
        }

        public IssueLabelModel(int id) : this()
        {
            var IssueLabelEntry = _issueLabelService.GetLabelTitleById(id);
            if (IssueLabelEntry != null)
            {
                Id = IssueLabelEntry.Id;
                LabelTitle = IssueLabelEntry.LabelTitle;
                ColorCode = IssueLabelEntry.ColorCode;

                CreatedAt = IssueLabelEntry.CreatedAt;
                CreatedBy = IssueLabelEntry.CreatedBy;
                CreatedByUser = IssueLabelEntry.CreatedByUser;
                UpdatedAt = IssueLabelEntry.UpdatedAt;
                UpdatedBy = IssueLabelEntry.UpdatedBy;
                UpdatedByUser = IssueLabelEntry.UpdatedByUser;
            }

        }
        public IEnumerable<IssueLabel> GetAllIssueLabel()
        {
            return _issueLabelService.GetAllIssueLabel();
        }

        public void AddIssueLabel()
        {
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;          
            _issueLabelService.AddIssuelabel(this);
        }
        public void EditIssueLabel()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _issueLabelService.EditIssueLabel(this);
        }
        public void DeleteIssueLabel(int id)
        {
            _issueLabelService.DeleteIssueLabel(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }

        public bool IsIssueLabelExist(string LabelTitle, string InitialLabelTitle)
        {
            return _issueLabelService.IsIssueLabelExist(LabelTitle, InitialLabelTitle);
        }
        //public bool CheckUserPosition(int userId, string positionName)
        //{
        //    return _positionService.CheckUserPosition(userId, positionName);
        //}
        //public bool CheckTicketProcessPosition(int userId)
        //{
        //    return _positionService.CheckTicketProcessPosition(userId);
        //}
        public void Dispose()
        {
            _issueLabelService.Dispose();
        }
    }
}