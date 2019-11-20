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
    [Table("IssueLabels")]
    public class IssueLabel : AuditableEntity
    {
       
        [Required]
        [Display(Name = "Label Title")]
        public string LabelTitle { get; set; }
        [Display(Name = "Color Code")]
        public string ColorCode { get; set; }

    }
}
