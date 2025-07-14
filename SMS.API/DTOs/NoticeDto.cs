using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class NoticeDto
    {
        public int NoticeId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int CreatedBy { get; set; }

        public TargetAudience TargetAudience { get; set; }

        public bool IsImportant { get; set; }
    }

    public class CreateNoticeDto
    {
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
        public TargetAudience TargetAudience { get; set; }
        public bool IsImportant { get; set; } = false;
    }

    public class UpdateNoticeDto : CreateNoticeDto
    {
        [Required]
        public int NoticeId { get; set; }
    }
}
