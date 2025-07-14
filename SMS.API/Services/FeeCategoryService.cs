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
    public class FeeCategoryService : IFeeCategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FeeCategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<FeeCategoryDto> CreateAsync(FeeCategoryDto feeCategoryDto)
        {
            var feeCategory = new FeeCategory
            {
                CategoryName = feeCategoryDto.CategoryName,
                Description = feeCategoryDto.Description
            };
            await _applicationDbContext.FeeCategories.AddAsync(feeCategory);
            await _applicationDbContext.SaveChangesAsync();
            return new FeeCategoryDto
            {
                FeeCategoryId = feeCategory.FeeCategoryId,
                CategoryName = feeCategory.CategoryName,
                Description = feeCategory.Description
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feeCategory = await _applicationDbContext.FeeCategories.FindAsync(id);
            if (feeCategory == null)
            {
                throw new KeyNotFoundException($"FeeCategory with ID {id} not found.");
            }
            _applicationDbContext.FeeCategories.Remove(feeCategory);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<FeeCategoryDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var feeCategories = await _applicationDbContext.FeeCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(f => new FeeCategoryDto
                {
                    FeeCategoryId = f.FeeCategoryId,
                    CategoryName = f.CategoryName,
                    Description = f.Description
                }).OrderByDescending(f => f.FeeCategoryId).ToListAsync();
            return feeCategories;
        }

        public async Task<FeeCategoryDto> GetByIdAsync(int id)
        {
            var feeCategory = await _applicationDbContext.FeeCategories
                .Where(f => f.FeeCategoryId == id)
                .Select(f => new FeeCategoryDto
                {
                    FeeCategoryId = f.FeeCategoryId,
                    CategoryName = f.CategoryName,
                    Description = f.Description
                }).FirstOrDefaultAsync();
            if (feeCategory == null)
            {
                 throw new KeyNotFoundException($"FeeCategory with ID {id} not found.");
            }
            return feeCategory;
        }

        public async Task<FeeCategoryDto> UpdateAsync(int id, FeeCategoryDto feeCategoryDto)
        {
            
            var feeCategory = await _applicationDbContext.FeeCategories.FindAsync(id);
            if (feeCategory == null)
            {
                throw new KeyNotFoundException($"FeeCategory with ID {id} not found.");
            }

            feeCategory.CategoryName = feeCategoryDto.CategoryName;
            feeCategory.Description = feeCategoryDto.Description;

            await _applicationDbContext.SaveChangesAsync();

            return new FeeCategoryDto
            {
                FeeCategoryId = feeCategory.FeeCategoryId,
                CategoryName = feeCategory.CategoryName,
                Description = feeCategory.Description
            };
        }
    }
}
