using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Entities
{
    [Table("AttachmentFiles")]
    public class AttachmentFile :AuditableEntity
    {
        [Required]
        [Display(Name = "FileName")]
        public string FileName { get; set; }

        [Display(Name = "OriginalName")]
        public string OriginalName { get; set; }

        [Display(Name = "FileExtension")]
        public string FileExtension { get; set; }

        [Display(Name = "FileLocation")]
        public string FileLocation { get; set; }
        [Display(Name="Ticket")]
        public int? TicketId { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }

    }
}
