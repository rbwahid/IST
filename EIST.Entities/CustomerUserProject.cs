using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Entities
{
    [Table("CustomerUserProjects")]
    public class CustomerUserProject: Entity
    {
        [Display(Name = "Customer User")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User CustomerUser { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
