using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class AcademicYear
    {
        [Key]
        public int AcademicYearId { get; set; }

        [Required]
        [MaxLength(20)]
        public string YearName { get; set; }  // e.g., "2024-2025"

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; } = false;

        public string Description { get; set; }
    }
}
