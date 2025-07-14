using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize);
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<CreateStudentDto> CreateStudentAsync(CreateStudentDto createStudent);
        Task<UpdateStudentDto> UpdateStudentAsync(int id, UpdateStudentDto updateStudent);
        Task<bool> DeleteStudentAsync(int id);
    }
}
