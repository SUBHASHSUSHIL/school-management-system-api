using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class ParentDto
    {
        public int ParentId { get; set; }

        public int UserId { get; set; }

        public string Occupation { get; set; }

        public decimal? AnnualIncome { get; set; }

        public string Education { get; set; }
    }

    public class CreateParentDto
    {
        public int ParentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Occupation { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal? AnnualIncome { get; set; }
        [MaxLength(100)]
        public string Education { get; set; }
    }

    public class UpdateParentDto : CreateParentDto
    {
        [Required]
        public int ParentId { get; set; }
    }
}
