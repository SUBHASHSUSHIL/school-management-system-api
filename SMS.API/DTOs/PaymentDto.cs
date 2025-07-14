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
    public class PaymentDto
    {
        public int PaymentId { get; set; }

        public int StudentFeeId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string TransactionReference { get; set; }

        public int ReceivedBy { get; set; }

        public string Remarks { get; set; }
    }

    public class CreatePaymentDto
    {
        [Required]
        public int StudentFeeId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [MaxLength(100)]
        public string TransactionReference { get; set; }
        [Required]
        public int ReceivedBy { get; set; }
        public string Remarks { get; set; }
    }

    public class UpdatePaymentDto : CreatePaymentDto
    {
        [Required]
        public int PaymentId { get; set; }
    }
}
