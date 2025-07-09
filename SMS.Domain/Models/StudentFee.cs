using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public enum PaymentStatus
    {
        Paid,
        Unpaid,
        Partial
    }

    public class StudentFee
    {
        [Key]
        public int StudentFeeId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int FeeStructureId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PaidAmount { get; set; } = 0.00m;

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        // 🔗 Navigation Properties
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("FeeStructureId")]
        public virtual FeeStructure FeeStructure { get; set; }
    }
}
