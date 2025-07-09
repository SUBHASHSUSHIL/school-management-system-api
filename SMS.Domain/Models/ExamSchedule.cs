using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class ExamSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        [Column(TypeName = "date")]
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

        // 🔗 Navigation Properties
        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}
