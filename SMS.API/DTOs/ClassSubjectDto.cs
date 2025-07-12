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
    public class ClassSubjectDto
    {
        public int ClassSubjectId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }

        public int? TeacherId { get; set; }
    }

    public class CreateClassSubjectDto
    {
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public int? TeacherId { get; set; }
    }
}
