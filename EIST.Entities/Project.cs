using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Entities
{
    [Table("Projects")]
    public class Project: AuditableEntity
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(Name = "Project Manager")]
        public int? PmId { get; set; }
        [ForeignKey("PmId")]
        public virtual User ProjectManager { get; set; }
        [Display(Name = "Supervisor")]
        public int? SuperVisorId { get; set; }
        [ForeignKey("SuperVisorId")]
        public virtual User SuperVisor { get; set; }

        public virtual ICollection<Issue> TicketCollections { get; set; }
        public virtual ICollection<CustomerUserProject> CustomerUserProjectCollections { get; set; }

    }
}
