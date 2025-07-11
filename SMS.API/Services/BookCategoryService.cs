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
    public class BookCategoryService : IBookCategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookCategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateBookCategoryDto> CreateBookCategoryAsync(CreateBookCategoryDto bookCategory)
        {
            var newBookCategory = new BookCategory
            {
                CategoryName = bookCategory.CategoryName,
                Description = bookCategory.Description
            };
            await _applicationDbContext.BookCategories.AddAsync(newBookCategory);
            await _applicationDbContext.SaveChangesAsync();
            return bookCategory;
        }

        public async Task<bool> DeleteBookCategoryAsync(int id)
        {
            
            var bookCategory = await _applicationDbContext.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return false;
            }
            _applicationDbContext.BookCategories.Remove(bookCategory);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookCategoryDto>> GetBookCategoriesAsync(int pageNumber, int pageSize)
        {
            var bookCategories = await _applicationDbContext.BookCategories.OrderByDescending(bc => bc.CategoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return bookCategories.Select(bc => new BookCategoryDto
            {
                CategoryId = bc.CategoryId,
                CategoryName = bc.CategoryName,
                Description = bc.Description
            }).ToList();
        }

        public async Task<BookCategoryDto> GetBookCategoryByIdAsync(int id)
        {
            var bookCategory = await _applicationDbContext.BookCategories
                .FirstOrDefaultAsync(bc => bc.CategoryId == id);
            if (bookCategory == null)
            {
                throw new KeyNotFoundException($"Book category with ID {id} not found.");
            }
            return new BookCategoryDto
            {
                CategoryId = bookCategory.CategoryId,
                CategoryName = bookCategory.CategoryName,
                Description = bookCategory.Description
            };
        }

        public async Task<CreateBookCategoryDto> UpdateBookCategoryAsync(int id, CreateBookCategoryDto bookCategory)
        {
            
            var existingCategory = await _applicationDbContext.BookCategories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Book category with ID {id} not found.");
            }

            existingCategory.CategoryName = bookCategory.CategoryName;
            existingCategory.Description = bookCategory.Description;

            await _applicationDbContext.SaveChangesAsync();
            return new CreateBookCategoryDto
            {
                CategoryName = existingCategory.CategoryName,
                Description = existingCategory.Description
            };
        }
    }
}
