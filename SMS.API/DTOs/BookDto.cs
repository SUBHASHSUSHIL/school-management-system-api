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
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public string Edition { get; set; }

        public int? BookCategoryId { get; set; }

        public decimal? Price { get; set; }

        public int Quantity { get; set; }

        public int AvailableQuantity { get; set; }

        public string ShelfNumber { get; set; }
        
    }

    public class CreateBookDto
    {
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        public string ISBN { get; set; }
        
        public string Publisher { get; set; }

        public string Edition { get; set; }

        public int? BookCategoryId { get; set; }

        public decimal? Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }

        public string ShelfNumber { get; set; }
    }
}
