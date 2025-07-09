using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class StudentHomework
    {
        [Key]
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

        // 🔗 Navigation Properties
        [ForeignKey("HomeworkId")]
        public Homework Homework { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }

    public enum HomeworkStatus
    {
        Assigned,
        Submitted,
        Late,
        Graded
    }
}
