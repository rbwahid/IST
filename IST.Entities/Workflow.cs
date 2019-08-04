using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Entities
{
    public class Workflow:AuditableEntity
    {
        [Required]
        public string FormName { get; set; }
        [Required]
        public int RecordId { get; set; }
        public int? PositionId { get; set; }
        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
        public int? ApproverId { get; set; }
        [ForeignKey("ApproverId")]
        public virtual User Approver { get; set; }
        public byte Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string Remarks { get; set; }
    }
}
