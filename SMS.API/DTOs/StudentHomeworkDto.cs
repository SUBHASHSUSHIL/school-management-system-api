using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class StudentHomeworkDto
    {
        public int StudentHomeworkId { get; set; }

        public int HomeworkId { get; set; }

        public int StudentId { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public int? MarksObtained { get; set; }

        public string Feedback { get; set; }

        public HomeworkStatus Status { get; set; }
    }

    public class CreateStudentHomeworkDto
    {
        public int StudentHomeworkId { get; set; }
        [Required]
        public int HomeworkId { get; set; }
        [Required]
        public int StudentId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? MarksObtained { get; set; }
        public string Feedback { get; set; }
        [Required]
        [EnumDataType(typeof(HomeworkStatus))]
        public HomeworkStatus Status { get; set; } = HomeworkStatus.Assigned;
    }

    public class UpdateStudentHomeworkDto : CreateStudentHomeworkDto
    {
        [Required]
        public int StudentHomeworkId { get; set; }
    }
}
