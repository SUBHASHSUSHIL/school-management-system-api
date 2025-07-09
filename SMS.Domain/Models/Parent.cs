using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(100)]
        public string Occupation { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? AnnualIncome { get; set; }

        [MaxLength(100)]
        public string Education { get; set; }

        // 🔗 Navigation Property
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
