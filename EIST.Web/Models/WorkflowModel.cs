using EIST.Common;
using EIST.Entities;
using EIST.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIST.Web.Models
{
    public class WorkflowModel:Workflow
    {
        private WorkflowService _workflowService;

        public WorkflowModel()
        {
            _workflowService = new WorkflowService();
        }

        public WorkflowModel(int id)
            : this()
        {
            var workflowApproval = _workflowService.GetWorkflowById(id);
            if (workflowApproval != null)
            {
                Id = workflowApproval.Id;
                ApproverId = workflowApproval.ApproverId;
                Approver = workflowApproval.Approver;
                PositionId = workflowApproval.PositionId;
                Position = workflowApproval.Position;
                RecordId = workflowApproval.RecordId;
                Remarks = workflowApproval.Remarks;
                ApprovalStatus = workflowApproval.ApprovalStatus;
                CreatedBy = workflowApproval.CreatedBy;
                CreatedAt = workflowApproval.CreatedAt;
                UpdatedAt = workflowApproval.UpdatedAt;
                UpdatedBy = workflowApproval.UpdatedBy;
            }
        }
        public byte Approve()
        {
            base.ApprovalStatus = "Accepted";
            base.Status = (byte)EnumTicketStatus.Accepted;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            var currStep = _workflowService.Approve(this);
            return currStep;
        }
        public void Disapprove()
        {
            base.ApprovalStatus = "Rejected";
            base.Status = (byte)EnumTicketStatus.Rejected;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _workflowService.Disapprove(this);
        }

        public Workflow GetWorkflowByPosition(int recordId, int positionId)
        {
            return _workflowService.GetWorkflowByPosition(recordId,positionId);
        }
        public IEnumerable<Workflow> GetWorkflowsByRecordId(int recordId,string formName)
        {
            return _workflowService.GetWorkflowsByRecordId(recordId,formName);
        }
        public void Dispose()
        {
            _workflowService.Dispose();
        }
    }
    public class WorkflowProcessModel
    {
        public int RecordId { get; set; }
        public int ApprovalId { get; set; }
        public string ApprovalRemarks { get; set; }
    }
}