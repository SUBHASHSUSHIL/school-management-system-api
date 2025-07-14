using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class FeeStructureService : IFeeStructureService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FeeStructureService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<FeeStructureCreateDto> CreateFeeStructureAsync(FeeStructureCreateDto createFeeStructureDto)
        {
            var feeStructure = new FeeStructure
            {
                ClassId = createFeeStructureDto.ClassId,
                FeeCategoryId = createFeeStructureDto.FeeCategoryId,
                Amount = createFeeStructureDto.Amount,
                AcademicYearId = createFeeStructureDto.AcademicYearId,
                DueDate = createFeeStructureDto.DueDate,
                IsActive = createFeeStructureDto.IsActive,
                Description = createFeeStructureDto.Description
            };

            _applicationDbContext.FeeStructures.Add(feeStructure);
            await _applicationDbContext.SaveChangesAsync();
            return new FeeStructureCreateDto
            {
                ClassId = feeStructure.ClassId,
                FeeCategoryId = feeStructure.FeeCategoryId,
                Amount = feeStructure.Amount,
                AcademicYearId = feeStructure.AcademicYearId,
                DueDate = feeStructure.DueDate,
                IsActive = feeStructure.IsActive,
                Description = feeStructure.Description
            };
        }

        public async Task<bool> DeleteFeeStructureAsync(int id)
        {
            var feeStructure = await _applicationDbContext.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
                throw new KeyNotFoundException($"FeeStructure with ID {id} not found.");
            }
            _applicationDbContext.FeeStructures.Remove(feeStructure);
            await _applicationDbContext.SaveChangesAsync();
            return true; 
        }

        public async Task<IEnumerable<FeeStructureDto>> GetAllFeeStructuresAsync(int pageNumber, int pageSize)
        {
            var feeStructures = await _applicationDbContext.FeeStructures
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(f => new FeeStructureDto
                {
                    FeeStructureId = f.FeeStructureId,
                    ClassId = f.ClassId,
                    FeeCategoryId = f.FeeCategoryId,
                    Amount = f.Amount,
                    AcademicYearId = f.AcademicYearId,
                    DueDate = f.DueDate,
                    IsActive = f.IsActive,
                    Description = f.Description
                }).OrderByDescending(f => f.FeeStructureId).ToListAsync();
            return feeStructures;
        }

        public async Task<FeeStructureDto> GetFeeStructureByIdAsync(int id)
        {
            var feeStructure = await _applicationDbContext.FeeStructures
                .Where(f => f.FeeStructureId == id)
                .Select(f => new FeeStructureDto
                {
                    FeeStructureId = f.FeeStructureId,
                    ClassId = f.ClassId,
                    FeeCategoryId = f.FeeCategoryId,
                    Amount = f.Amount,
                    AcademicYearId = f.AcademicYearId,
                    DueDate = f.DueDate,
                    IsActive = f.IsActive,
                    Description = f.Description
                }).FirstOrDefaultAsync();
            return feeStructure;
        }

        public async Task<FeeStructureUpdateDto> UpdateFeeStructureAsync(int id, FeeStructureUpdateDto updateFeeCategoryDto)
        {
            var feeStructure = await _applicationDbContext.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
               throw new KeyNotFoundException($"FeeStructure with ID {id} not found.");
            }
            feeStructure.ClassId = updateFeeCategoryDto.ClassId;
            feeStructure.FeeCategoryId = updateFeeCategoryDto.FeeCategoryId;
            feeStructure.Amount = updateFeeCategoryDto.Amount;
            feeStructure.AcademicYearId = updateFeeCategoryDto.AcademicYearId;
            feeStructure.DueDate = updateFeeCategoryDto.DueDate;
            feeStructure.IsActive = updateFeeCategoryDto.IsActive;
            feeStructure.Description = updateFeeCategoryDto.Description;

            _applicationDbContext.FeeStructures.Update(feeStructure);
            await _applicationDbContext.SaveChangesAsync();

            return new FeeStructureUpdateDto
            {
                ClassId = feeStructure.ClassId,
                FeeCategoryId = feeStructure.FeeCategoryId,
                Amount = feeStructure.Amount,
                AcademicYearId = feeStructure.AcademicYearId,
                DueDate = feeStructure.DueDate,
                IsActive = feeStructure.IsActive,
                Description = feeStructure.Description
            };
        }
    }
}
