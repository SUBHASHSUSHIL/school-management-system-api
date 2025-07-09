using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required, MaxLength(10)]
        public string SectionName { get; set; }

        [Required]
        public int ClassId { get; set; }

        public int? Capacity { get; set; }

        public int? ClassTeacherId { get; set; }

        [MaxLength(20)]
        public string RoomNumber { get; set; }

        // 🔗 Navigation Properties
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [ForeignKey("ClassTeacherId")]
        public virtual Teacher ClassTeacher { get; set; }
    }
}
