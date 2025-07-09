using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public int? MaxMarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔗 Navigation Properties
        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}
