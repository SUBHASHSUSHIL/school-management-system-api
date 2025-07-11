using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IBookCategoryService
    {
        Task<List<BookCategoryDto>> GetBookCategoriesAsync(int pageNumber, int pageSize);
        Task<BookCategoryDto> GetBookCategoryByIdAsync(int id);
        Task<CreateBookCategoryDto> CreateBookCategoryAsync(CreateBookCategoryDto bookCategory);
        Task<CreateBookCategoryDto> UpdateBookCategoryAsync(int id, CreateBookCategoryDto bookCategory);
        Task<bool> DeleteBookCategoryAsync(int id);
    }
}
