using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class FeeStructure
    {
        [Key]
        public int FeeStructureId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int FeeCategoryId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int AcademicYearId { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsActive { get; set; } = true;

        public string Description { get; set; }

        // 🔗 Navigation Properties
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [ForeignKey("FeeCategoryId")]
        public virtual FeeCategory FeeCategory { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual AcademicYear AcademicYear { get; set; }
    }
}
