using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.DTOs
{
    public class StaffDto
    {
        public int StaffId { get; set; }

        public int UserId { get; set; }

        public string EmployeeId { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }
    }

    public class CreateStaffDto
    {
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
    }

    public class UpdateStaffDto : CreateStaffDto
    {
        [Required]
        public int StaffId { get; set; }
    }
}
