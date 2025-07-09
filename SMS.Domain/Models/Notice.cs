using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Notice
    {
        [Key]
        public int NoticeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        [EnumDataType(typeof(TargetAudience))]
        public TargetAudience TargetAudience { get; set; }

        public bool IsImportant { get; set; } = false;

        // 🔗 Navigation Property
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
    }

    public enum TargetAudience
    {
        All,
        Teachers,
        Students,
        Parents,
        Staff
    }
}
