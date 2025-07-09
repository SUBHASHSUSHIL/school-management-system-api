using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required, MaxLength(50)]
        public string ClassName { get; set; }

        [Required]
        public int ClassNumeric { get; set; }

        public int? TeacherInChargeId { get; set; }

        public string Description { get; set; }

        // 🔗 Navigation Property
        [ForeignKey("TeacherInChargeId")]
        public virtual Teacher TeacherInCharge { get; set; }
    }
}
