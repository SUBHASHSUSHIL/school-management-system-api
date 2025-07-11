using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IBookIssueService
    {
        Task<List<BookIssueDto>> GetBookIssueAsync(int pageNumber, int pageSize);
        Task<BookIssueDto> GetByIdAsync(int id);
        Task<CreateBookIssueDto> CreateBookAsync(CreateBookIssueDto bookIssueDto);
        Task<BookIssueDto> UpdateBookAsync(int id, CreateBookIssueDto bookIssueDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
