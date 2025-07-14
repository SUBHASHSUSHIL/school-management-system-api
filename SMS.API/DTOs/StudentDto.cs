using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class StudentDto
    {
        public int StudentId { get; set; }

        public int UserId { get; set; }

        public string AdmissionNumber { get; set; }

        public DateTime AdmissionDate { get; set; }

        public int? CurrentClassId { get; set; }

        public int? CurrentSectionId { get; set; }

        public int? ParentId { get; set; }

        public string BloodGroup { get; set; }

        public string MedicalConditions { get; set; }
    }

    public class CreateStudentDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(20)]
        public string AdmissionNumber { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }
        public int? CurrentClassId { get; set; }
        public int? CurrentSectionId { get; set; }
        public int? ParentId { get; set; }
        [MaxLength(5)]
        public string BloodGroup { get; set; }
        public string MedicalConditions { get; set; }
    }

    public class UpdateStudentDto : CreateStudentDto
    {
        [Required]
        public int StudentId { get; set; }
    }
}
