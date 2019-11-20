using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Entities
{
    [Table("TicketAssigns")]
    public class TicketAssign: AuditableEntity
    {
        [Display(Name = "Issue")]
        public int? IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }

        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }


        [Display(Name = "Assignee")]
        public int AssigneeId { get; set; }
        [ForeignKey("AssigneeId")]
        public virtual User Assignee { get; set; }
    }
}
