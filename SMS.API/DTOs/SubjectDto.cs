using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public string SubjectCode { get; set; }

        public string Description { get; set; }

        public bool IsCore { get; set; }
    }

    public class CreateSubjectDto
    {
        public int SubjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SubjectCode { get; set; }
        public string Description { get; set; }
        public bool IsCore { get; set; } = true;
    }

    public class UpdateSubjectDto : CreateSubjectDto
    {
        [Required]
        public int SubjectId { get; set; }
    }
}
