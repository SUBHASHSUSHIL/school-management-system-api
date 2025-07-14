using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IExamResultService
    {
        Task<IEnumerable<ExamResultDto>> GetAllExamResultsAsync(int pageNumber, int pageSize);
        Task<ExamResultDto> GetExamResultByIdAsync(int id);
        Task<ExamResultCreateDto> CreateExamResultAsync(ExamResultCreateDto examResultCreateDto);
        Task<ExamResultUpdateDto> UpdateExamResultAsync(int id, ExamResultUpdateDto examResultUpdateDto);
        Task<bool> DeleteExamResultAsync(int id);
    }
}
