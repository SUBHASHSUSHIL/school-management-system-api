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
    public class ExamScheduleDto
    {
        public int ScheduleId { get; set; }

        public int ExamId { get; set; }

        public int SubjectId { get; set; }

        public int ClassId { get; set; }

        public DateTime ExamDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public decimal MaxMarks { get; set; }

        public decimal PassingMarks { get; set; }

        public string RoomNumber { get; set; }
    }

    public class CreateExamScheduleDto
    {
        [Required]
        public int ExamId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public DateTime ExamDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal MaxMarks { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal PassingMarks { get; set; }
        [MaxLength(20)]
        public string RoomNumber { get; set; }
    }

    public class UpdateExamScheduleDto : CreateExamScheduleDto
    {
        [Required]
        public int ScheduleId { get; set; }
    }
}
