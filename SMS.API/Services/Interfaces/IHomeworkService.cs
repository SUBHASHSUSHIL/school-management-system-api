using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IHomeworkService
    {
        Task<IEnumerable<HomeworkDto>> GetAllHomeworksAsync(int pageNumber = 1, int pageSize = 10);
        Task<HomeworkDto> GetHomeworkByIdAsync(int id);
        Task<CreateHomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto);
        Task<UpdateHomeworkDto> UpdateHomeworkAsync(int id, UpdateHomeworkDto homeworkDto);
        Task<bool> DeleteHomeworkAsync(int id);
    }
}
