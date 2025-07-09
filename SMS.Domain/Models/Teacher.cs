using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SMS.Domain.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(20)]
        public string EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime JoiningDate { get; set; }

        public string Qualification { get; set; }

        [MaxLength(100)]
        public string Specialization { get; set; }

        public int? ExperienceYears { get; set; }

        public bool IsClassTeacher { get; set; } = false;

        public int? ClassTeacherOfClassId { get; set; }

        public int? ClassTeacherOfSectionId { get; set; }

        // 🔗 Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ClassTeacherOfClassId")]
        public virtual Class ClassTeacherOfClass { get; set; }

        [ForeignKey("ClassTeacherOfSectionId")]
        public virtual Section ClassTeacherOfSection { get; set; }
    }
}
