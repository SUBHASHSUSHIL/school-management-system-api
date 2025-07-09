using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class BookIssue
    {
        [Key]
        public int IssueId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [EnumDataType(typeof(IssueStatus))]
        public IssueStatus Status { get; set; } = IssueStatus.Issued;

        [Column(TypeName = "decimal(10,2)")]
        public decimal FineAmount { get; set; } = 0.00M;

        public string Remarks { get; set; }

        [Required]
        public int IssuedBy { get; set; }

        // 🔗 Navigation Properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("IssuedBy")]
        public virtual User Issuer { get; set; }
    }

    public enum IssueStatus
    {
        Issued,
        Returned,
        Overdue,
        Lost
    }
}
