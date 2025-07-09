using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(20)]
        public string EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime JoiningDate { get; set; }

        [Required, MaxLength(100)]
        public string Position { get; set; }

        [MaxLength(100)]
        public string Department { get; set; }

        // 🔗 Navigation Property
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
