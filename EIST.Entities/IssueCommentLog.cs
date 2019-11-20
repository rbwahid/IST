using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Entities
{
    [Table("IssueCommentLog")]
    public class IssueCommentLog : AuditableEntity
    {
        [Display(Name = "Issue")]
        public int? IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }
        public byte StepOrder { get; set; }
        public bool IsInvalid { get; set; }
        public bool Internal { get; set; }

  
    }
}
