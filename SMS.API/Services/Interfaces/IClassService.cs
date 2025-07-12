using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IClassService
    {
        Task<List<ClassDto>> GetClassesAsync(int pageNumber, int pageSize);
        Task<ClassDto> GetClassByIdAsync(int classId);
        Task<CreateClassDto> CreateClassAsync(CreateClassDto createClassDto);
        Task<UpdateClassDto> UpdateClassAsync(int classId, UpdateClassDto updateClassDto);
        Task<bool> DeleteClassAsync(int classId);
    }
}
