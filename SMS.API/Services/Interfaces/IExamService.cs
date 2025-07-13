using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IExamService
    {
        Task<List<ExamDto>> GetAllExamAsync(int pageNumber, int pageSize);
        Task<ExamDto> GetExamByIdAsync(int id);
        Task<CreateExamDto> CreateExamAsync(CreateExamDto createEventDto);
        Task<CreateExamDto> UpdateExamAsync(int id, CreateExamDto updateEventDto);
        Task<bool> DeleteExamAsync(int id);
    }
}
