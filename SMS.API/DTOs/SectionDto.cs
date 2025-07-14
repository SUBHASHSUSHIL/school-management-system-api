using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class SectionDto
    {
        public int SectionId { get; set; }

        public string SectionName { get; set; }

        public int ClassId { get; set; }

        public int? Capacity { get; set; }

        public int? ClassTeacherId { get; set; }

        public string RoomNumber { get; set; }
    }

    public class CreateSectionDto
    {
        public int SectionId { get; set; }

        [Required, MaxLength(10)]
        public string SectionName { get; set; }
        [Required]
        public int ClassId { get; set; }
        public int? Capacity { get; set; }
        public int? ClassTeacherId { get; set; }
        [MaxLength(20)]
        public string RoomNumber { get; set; }
    }

    public class UpdateSectionDto : CreateSectionDto
    {
        [Required]
        public int SectionId { get; set; }
    }
}
