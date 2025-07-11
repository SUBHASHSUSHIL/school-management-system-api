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
    public class ClassDto
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public int ClassNumeric { get; set; }

        public int? TeacherInChargeId { get; set; }

        public string Description { get; set; }
    }

    public class CreateClassDto
    {
        [Required(ErrorMessage = "Class name is required.")]
        [MaxLength(50, ErrorMessage = "Class name cannot exceed 50 characters.")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Class numeric value is required.")]
        [Range(1, 12, ErrorMessage = "Class numeric must be between 1 to 12.")]
        public int ClassNumeric { get; set; }

        public int? TeacherInChargeId { get; set; }

        public string Description { get; set; }
    }
}
