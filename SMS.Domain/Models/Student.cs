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
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

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

        // 🔗 Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CurrentClassId")]
        public virtual Class CurrentClass { get; set; }

        [ForeignKey("CurrentSectionId")]
        public virtual Section CurrentSection { get; set; }

        [ForeignKey("ParentId")]
        public virtual Parent Parent { get; set; }
    }
}
