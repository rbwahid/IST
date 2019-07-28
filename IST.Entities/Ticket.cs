using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Entities
{
    [Table("Tickets")]
    public class Ticket: AuditableEntity
    {
        [Required]
        [Display(Name = "IssueName")]
        public string IssueName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Priority")]
        public bool Priority { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }


        [Display(Name = "Attachment")]
        public int? AttachmentFileId { get; set; }
        [ForeignKey("AttachmentFileId")]
        public virtual AttachmentFile AttachmentFile  { get; set; }

        [Display(Name = "Project")]
        public int? CompanyProjectId { get; set; }
        [ForeignKey("CompanyProjectId")]
        public virtual CompanyProject CompanyProject  { get; set; }


    }
}
