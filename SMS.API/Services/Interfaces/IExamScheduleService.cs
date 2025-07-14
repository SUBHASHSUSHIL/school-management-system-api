using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IExamScheduleService
    {
        Task<IEnumerable<ExamScheduleDto>> GetAllExamSchedulesAsync(int pageNumber, int pageSize);
        Task<ExamScheduleDto> GetExamScheduleByIdAsync(int scheduleId);
        Task<CreateExamScheduleDto> CreateExamScheduleAsync(CreateExamScheduleDto createExamScheduleDto);
        Task<UpdateExamScheduleDto> UpdateExamScheduleAsync(int scheduleId, UpdateExamScheduleDto updateExamScheduleDto);
        Task<bool> DeleteExamScheduleAsync(int scheduleId);
    }
}
