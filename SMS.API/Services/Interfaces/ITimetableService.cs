using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface ITimetableService
    {
        Task<IEnumerable<TimetableDto>> GetAllTimetablesAsync(int pageNumber, int pageSize);
        Task<TimetableDto> GetTimetableByIdAsync(int id);
        Task<CreateTimetableDto> CreateTimetableAsync(CreateTimetableDto createTimetable);
        Task<UpdateTimetableDto> UpdateTimetableAsync(int id, UpdateTimetableDto updateTimetable);
        Task<bool> DeleteTimetableAsync(int id);
    }
}
