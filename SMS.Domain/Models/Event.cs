using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string Organizer { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔗 Navigation Property
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
    }
}

