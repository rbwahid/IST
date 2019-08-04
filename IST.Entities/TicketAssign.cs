using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Entities
{
    [Table("TicketAssigns")]
    public class TicketAssign: AuditableEntity
    {
        [Display(Name = "Ticket Issue")]
        public int? TicketId { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }

        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }
        [Display(Name = "Approved Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }

        [Display(Name = "Developer")]
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
