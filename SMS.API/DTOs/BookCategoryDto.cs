using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class BookCategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }

    public class CreateBookCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
