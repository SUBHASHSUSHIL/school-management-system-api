using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class FeeCategoryDto
    {
        public int FeeCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }

    public class CreateFeeCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
     
    public class UpdateFeeCategoryDto : CreateFeeCategoryDto
    {
        
    }
}
