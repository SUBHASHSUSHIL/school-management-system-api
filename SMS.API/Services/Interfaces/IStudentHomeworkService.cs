using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IStudentHomeworkService
    {
        Task<IEnumerable<StudentHomeworkDto>> GetAllStudentHomeworksAsync(int pageNumber, int pageSize);
        Task<StudentHomeworkDto> GetStudentHomeworkByIdAsync(int id);
        Task<CreateStudentHomeworkDto> CreateStudentHomeworkAsync(CreateStudentHomeworkDto createStudentHomework);
        Task<UpdateStudentHomeworkDto> UpdateStudentHomeworkAsync(int id, UpdateStudentHomeworkDto updateStudentHomework);
        Task<bool> DeleteStudentHomeworkAsync(int id);
    }
}
