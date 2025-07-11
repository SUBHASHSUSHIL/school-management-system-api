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
    public class BookIssueDto
    {
        public int IssueId { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public IssueStatus Status { get; set; }

        public decimal FineAmount { get; set; }

        public string Remarks { get; set; }

        public int IssuedBy { get; set; }
    }

    public class CreateBookIssueDto
    {
        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public IssueStatus Status { get; set; }

        public decimal FineAmount { get; set; }

        public string Remarks { get; set; }

        public int IssuedBy { get; set; }
    }
}
