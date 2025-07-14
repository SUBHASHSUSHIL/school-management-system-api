using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class FeeStructureDto
    {
        public int FeeStructureId { get; set; }

        public int ClassId { get; set; }

        public int FeeCategoryId { get; set; }

        public decimal Amount { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }
    }

    public class FeeStructureCreateDto
    {
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
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class FeeStructureUpdateDto : FeeStructureCreateDto
    {
        [Required]
        public int FeeStructureId { get; set; }
        public bool IsActive { get; set; }
    }
}
