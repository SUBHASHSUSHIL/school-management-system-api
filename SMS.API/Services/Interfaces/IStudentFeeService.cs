using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IStudentFeeService
    {
        Task<IEnumerable<StudentFeeDto>> GetAllStudentFeesAsync(int pageNumber, int pageSize);
        Task<StudentFeeDto> GetStudentFeeByIdAsync(int studentFeeId);
        Task<CreateStudentFeeDto> CreateStudentFeeAsync(CreateStudentFeeDto createStudentFeeDto);
        Task<UpdateStudentFeeDto> UpdateStudentFeeAsync(int studentFeeId, UpdateStudentFeeDto updateStudentFeeDto);
        Task<bool> DeleteStudentFeeAsync(int studentFeeId);
    }
}
