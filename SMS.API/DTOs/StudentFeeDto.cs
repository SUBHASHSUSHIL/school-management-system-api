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
    public class StudentFeeDto
    {
        public int StudentFeeId { get; set; }

        public int StudentId { get; set; }

        public int FeeStructureId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public decimal PaidAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }

    public class CreateStudentFeeDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int FeeStructureId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public decimal PaidAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }

    public class UpdateStudentFeeDto : CreateStudentFeeDto
    {
        [Required]
        public int StudentFeeId { get; set; }
    }
}
