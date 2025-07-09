using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class ExamResult
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal MarksObtained { get; set; }

        [MaxLength(5)]
        public string Grade { get; set; }

        public string Remarks { get; set; }

        [Required]
        public int RecordedBy { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.Now;

        // 🔗 Navigation Properties
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("RecordedBy")]
        public virtual User Recorder { get; set; }
    }
}
