using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }

        public int UserId { get; set; }

        public string EmployeeId { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Qualification { get; set; }

        public string Specialization { get; set; }

        public int? ExperienceYears { get; set; }

        public bool IsClassTeacher { get; set; } = false;

        public int? ClassTeacherOfClassId { get; set; }

        public int? ClassTeacherOfSectionId { get; set; }
    }

    public class CreateTeacherDto
    {
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
    }

    public class UpdateTeacherDto : CreateTeacherDto
    {
        [Required]
        public int TeacherId { get; set; }
    }
}
