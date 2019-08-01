using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Entities
{
    public class Position:AuditableEntity
    {
        [Required]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
    }
}
