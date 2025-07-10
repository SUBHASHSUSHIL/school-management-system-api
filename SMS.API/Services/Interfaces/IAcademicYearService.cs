using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IAcademicYearService
    {
        Task<List<AcademicYear>> GetAllAcademicYearsAsync(int pageNumber, int pageSize);
        Task<AcademicYear> GetAcademicYearByIdAsync(int academicYearId);
        Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear);
        Task<AcademicYear> UpdateAcademicYearAsync(int academicYearId, AcademicYear academicYear);
        Task<bool> DeleteAcademicYearAsync(int academicYearId);
    }
}
