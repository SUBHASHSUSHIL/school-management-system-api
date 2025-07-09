using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; }

        [MaxLength(20)]
        public string Edition { get; set; }

        public int? CategoryId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }

        [MaxLength(20)]
        public string ShelfNumber { get; set; }

        // 🔗 Navigation Property
        [ForeignKey("CategoryId")]
        public virtual BookCategory Category { get; set; }
    }
}
