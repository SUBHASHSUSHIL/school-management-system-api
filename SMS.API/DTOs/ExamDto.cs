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
    public class ExamDto
    {
        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }
    }

    public class CreateExamDto
    {
        public string ExamName { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }
    }
}
