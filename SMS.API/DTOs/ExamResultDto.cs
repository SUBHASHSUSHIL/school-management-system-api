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
    public class ExamResultDto
    {
        public int ResultId { get; set; }

        public int StudentId { get; set; }

        public int ExamId { get; set; }

        public int SubjectId { get; set; }

        public decimal MarksObtained { get; set; }

        public string Grade { get; set; }

        public string Remarks { get; set; }

        public int RecordedBy { get; set; }

        public DateTime RecordedAt { get; set; }
    }

    public class ExamResultCreateDto
    {
        public int ResultId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ExamId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal MarksObtained { get; set; }
        [MaxLength(5)]
        public string Grade { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int RecordedBy { get; set; }
        public DateTime RecordedAt { get; set; }
    }

    public class ExamResultUpdateDto : ExamResultCreateDto
    {
    }
}
