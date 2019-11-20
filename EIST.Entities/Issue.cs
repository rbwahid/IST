using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EIST.Entities
{
    [Table("Issues")]
    public class Issue: AuditableEntity
    {
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Issue Title")]
        public string IssueTitle { get; set; }
        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Priority")]
        public bool Priority { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }
        [Display(Name = "Approved Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }
        [Display(Name = "Project")]
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project  { get; set; }
        [Display(Name = "Project")]
        public int? LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual IssueLabel IssueLabel { get; set; }
        public  String Milestone { get; set; }
        public  bool? IsClosed { get; set; }

        public virtual ICollection<AttachmentFile> AttachmentFileCollection { get; set; }
        public virtual ICollection<TicketAssign> TicketAssignCollection { get; set; }
    }
}
