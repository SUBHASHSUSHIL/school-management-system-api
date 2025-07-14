using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class TimetableDto
    {
        public int TimetableId { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public DayOfWeekEnum DayOfWeek { get; set; }

        public int PeriodNumber { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string RoomNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CreateTimetableDto
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
        public DayOfWeekEnum DayOfWeek { get; set; }
        [Required]
        public int PeriodNumber { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [MaxLength(20)]
        public string RoomNumber { get; set; }
    }

    public class UpdateTimetableDto : CreateTimetableDto
    {
        [Required]
        public int TimetableId { get; set; }
    }
}
