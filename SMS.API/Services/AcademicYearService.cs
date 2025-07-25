using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AcademicYearService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear)
        {
            var newAcademicYear = new AcademicYear
            {
                YearName = academicYear.YearName,
                StartDate = academicYear.StartDate,
                EndDate = academicYear.EndDate,
                IsCurrent = academicYear.IsCurrent,
                Description = academicYear.Description
            };
            _applicationDbContext.AcademicYears.Add(newAcademicYear);
            await _applicationDbContext.SaveChangesAsync();
            return newAcademicYear;
        }

        public async Task<bool> DeleteAcademicYearAsync(int academicYearId)
        {
            var academicYear = await _applicationDbContext.AcademicYears
                .FirstOrDefaultAsync(a => a.AcademicYearId == academicYearId);
            if (academicYear == null)
            {
                throw new KeyNotFoundException($"Academic Year with ID {academicYearId} not found.");
            }
            return true;
        }

        public async Task<AcademicYear> GetAcademicYearByIdAsync(int academicYearId)
        {
            var academicYear = await _applicationDbContext.AcademicYears
                .FirstOrDefaultAsync(a => a.AcademicYearId == academicYearId);
            if (academicYear == null)
            {
                throw new KeyNotFoundException($"Academic Year with ID {academicYearId} not found.");
            }
            return academicYear;
        }

        public async Task<List<AcademicYear>> GetAllAcademicYearsAsync(int pageNumber, int pageSize)
        {
            var academicYears = await _applicationDbContext.AcademicYears.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderByDescending(a => a.AcademicYearId)
                .ToListAsync();
            return academicYears;
        }

        public async Task<AcademicYear> UpdateAcademicYearAsync(int academicYearId, AcademicYear academicYear)
        {
            var existingAcademicYear = await _applicationDbContext.AcademicYears
                .FirstOrDefaultAsync(a => a.AcademicYearId == academicYearId);
            if (existingAcademicYear == null)
            {
                throw new KeyNotFoundException($"Academic Year with ID {academicYearId} not found.");
            }
            existingAcademicYear.YearName = academicYear.YearName;
            existingAcademicYear.StartDate = academicYear.StartDate;
            existingAcademicYear.EndDate = academicYear.EndDate;
            existingAcademicYear.IsCurrent = academicYear.IsCurrent;
            existingAcademicYear.Description = academicYear.Description;
            _applicationDbContext.AcademicYears.Update(existingAcademicYear);
            await _applicationDbContext.SaveChangesAsync();
            return existingAcademicYear;
        }
    }
}
