using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public enum PaymentMethod
    {
        Cash,
        Cheque,
        CreditCard,
        DebitCard,
        BankTransfer,
        OnlinePayment
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

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

        // 🔗 Navigation properties
        [ForeignKey("StudentFeeId")]
        public virtual StudentFee StudentFee { get; set; }

        [ForeignKey("ReceivedBy")]
        public virtual User ReceivedByUser { get; set; }
    }
}
