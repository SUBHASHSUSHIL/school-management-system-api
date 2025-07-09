using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class FeeCategory
    {
        [Key]
        public int FeeCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
