using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Entities
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
        [Display(Name="Issue")]
        public int? IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
        [Display(Name = "Comment")]
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual IssueCommentLog Comment { get; set; }

    }
}
