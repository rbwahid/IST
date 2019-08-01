using IST.Entities;
using IST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Service
{
    public class WorkflowService
    {
        private ISTDbContext _context;
        private WorkflowUnitOfWork _workflowApprovalUnitOfWork;

        public WorkflowService()
        {
            _context = new ISTDbContext();
            _workflowApprovalUnitOfWork = new WorkflowUnitOfWork(_context);
        }

        public IEnumerable<Workflow> GetAllWorkflow()
        {
            return _workflowApprovalUnitOfWork.WorkflowRepository.GetAll().Where(x => x.Status != 0);
        }
        public Workflow GetWorkflowById(int id)
        {
            Workflow workflowApproval = _workflowApprovalUnitOfWork.WorkflowRepository.GetById(id);
            return workflowApproval;
        }
        public Workflow GetWorkflowByPosition(int recordId,int positionId)
        {
            return _workflowApprovalUnitOfWork.WorkflowRepository.GetWorkflowByPosition(recordId,positionId);
        }
        public IEnumerable<Workflow> GetWorkflowsByRecordId(int recordId)
        {
            return _workflowApprovalUnitOfWork.WorkflowRepository.GetWorkflowsByRecordId(recordId);
        }
        public int AddWorkflow(Workflow workflow)
        {
            var newWorkflow = new Workflow
            {
                RecordId = workflow.RecordId,
                ApproverId = workflow.ApproverId,
                PositionId = workflow.PositionId,
                Remarks = workflow.Remarks,
                Status = workflow.Status,
                ApprovalStatus = workflow.ApprovalStatus,
                CreatedBy = workflow.CreatedBy,
                CreatedAt = workflow.CreatedAt,
            };
            _workflowApprovalUnitOfWork.WorkflowRepository.Add(newWorkflow);
            _workflowApprovalUnitOfWork.Save();
            return newWorkflow.RecordId;
        }
        public void EditWorkflow(Workflow workflow)
        {
            var workflowApprovalEntry = GetWorkflowById(workflow.Id);
            if(workflowApprovalEntry != null)
            {
                workflowApprovalEntry.RecordId = workflow.RecordId;
                workflowApprovalEntry.ApproverId = workflow.ApproverId;
                workflowApprovalEntry.PositionId = workflow.PositionId;
                workflowApprovalEntry.Remarks = workflow.Remarks;
                workflowApprovalEntry.Status = workflow.Status;
                workflowApprovalEntry.ApprovalStatus = workflow.ApprovalStatus;
                workflowApprovalEntry.UpdatedAt = DateTime.Now;
                workflowApprovalEntry.UpdatedBy = workflow.UpdatedBy;
                _workflowApprovalUnitOfWork.Save();
            }
        }
        public byte Approve(Workflow workflow)
        {
            var workflowApprovalEntry = GetWorkflowById(workflow.Id);
            if(workflowApprovalEntry != null)
            {
                workflowApprovalEntry.Remarks = workflow.Remarks;
                workflowApprovalEntry.ApprovalStatus = workflow.ApprovalStatus;
                workflowApprovalEntry.UpdatedAt = DateTime.Now;
                workflowApprovalEntry.UpdatedBy = workflow.UpdatedBy;
                //_workflowApprovalUnitOfWork.SetCurrentStep(workflowApprovalEntry.FormId, workflowApprovalEntry.RecordId, workflow.CurrentStep);
                _workflowApprovalUnitOfWork.Save(); 
            }
            return workflow.Status;
        }
        public void Disapprove(Workflow workflow)
        {
            var workflowApprovalEntry = GetWorkflowById(workflow.Id);
            workflowApprovalEntry.Remarks = workflow.Remarks;
            workflowApprovalEntry.ApprovalStatus = workflow.ApprovalStatus;
            workflowApprovalEntry.UpdatedAt = DateTime.Now;
            workflowApprovalEntry.UpdatedBy = workflow.UpdatedBy;
            _workflowApprovalUnitOfWork.Save();
        }
        public void Dispose()
        {
            _workflowApprovalUnitOfWork.Dispose();
        }
    }
}
