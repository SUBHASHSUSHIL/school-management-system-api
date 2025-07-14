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
    public class HomeworkDto
    {
        public int HomeworkId { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime DueDate { get; set; }

        public int? MaxMarks { get; set; }

        public DateTime CreatedAt { get; set; }        
    }

    public class CreateHomeworkDto
    {
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
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateHomeworkDto : CreateHomeworkDto
    {
        public int HomeworkId { get; set; }
    }
}
