using EIST.Entities;
using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Service
{
    public class IssueLabelService
    {
        private EISTDbContext _context;
        public IssueLabelUnitOfWork _issuelabelUnitOfWork;

        public IssueLabelService()
        {
            _context = new EISTDbContext();
            _issuelabelUnitOfWork = new IssueLabelUnitOfWork(_context);
        }
        public IssueLabel GetLabelTitleById(int id)
        {
            return _issuelabelUnitOfWork.issueLabelRepository.GetById(id);
        }
        public IssueLabel GetLabelTitleIdByName(string LabelTitle)
        {
            return _issuelabelUnitOfWork.issueLabelRepository.GetIssuelabelByName(LabelTitle);
        }
        public IEnumerable<IssueLabel> GetAllIssueLabel()
        {
            return _issuelabelUnitOfWork.issueLabelRepository.GetAllIssueLabel();
        }
        public void AddIssuelabel(IssueLabel issueLabel)
        {
            var newIssueLabel = new IssueLabel
            {
                LabelTitle = issueLabel.LabelTitle,
                ColorCode = issueLabel.ColorCode,
                CreatedAt = issueLabel.CreatedAt,
                CreatedBy = issueLabel.CreatedBy
            };
            _issuelabelUnitOfWork.issueLabelRepository.Add(newIssueLabel);
            _issuelabelUnitOfWork.Save();
        }
        public void EditIssueLabel(IssueLabel issueLabel)
        {
            var issueLabelEntry = GetLabelTitleById(issueLabel.Id);
            if (issueLabelEntry != null)
            {
                issueLabelEntry.LabelTitle = issueLabel.LabelTitle;
                issueLabelEntry.ColorCode = issueLabel.ColorCode;
                issueLabelEntry.UpdatedAt = issueLabel.UpdatedAt;
                issueLabelEntry.UpdatedBy = issueLabel.UpdatedBy;
                _issuelabelUnitOfWork.Save();
            }
        }

        public void DeleteIssueLabel(int id, string currUserId)
        {
            _issuelabelUnitOfWork.issueLabelRepository.Disable(id);
            _issuelabelUnitOfWork.Save(currUserId);
        }
        public bool IsIssueLabelExist(string LabelTitle, string InitialLabelTitle)
        {
            return _issuelabelUnitOfWork.issueLabelRepository.IsIssueLabelExist(LabelTitle, InitialLabelTitle);
        }
        public void Dispose()
        {
            _issuelabelUnitOfWork.Dispose();
        }
    }
}
