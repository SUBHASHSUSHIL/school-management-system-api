using SMS.API.DTOs;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetBooksAsync(int pageNumber, int pageSize);
        Task<BookDto> GetBookByIdAsync(int id);
        Task<CreateBookDto> CreateBookAsync(CreateBookDto book);
        Task<CreateBookDto> UpdateBookAsync(int id, CreateBookDto book);
        Task<bool> DeleteBookAsync(int id);
    }
}
