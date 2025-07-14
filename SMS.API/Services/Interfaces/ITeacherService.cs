using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync(int pageNumber, int pageSize);
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<CreateTeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacher);
        Task<UpdateTeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto updateTeacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
