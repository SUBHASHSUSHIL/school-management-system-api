using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IClassSubjectService
    {
        Task<List<ClassSubjectDto>> GetClassSubjectsAsync(int pageNumber, int pageSize);
        Task<ClassSubjectDto> GetClassSubjectByIdAsync(int id);
        Task<CreateClassSubjectDto> CreateClassSubjectAsync(CreateClassSubjectDto classSubjectDto);
        Task<ClassSubjectDto> UpdateClassSubjectAsync(int id, CreateClassSubjectDto classSubjectDto);
        Task<bool> DeleteClassSubjectAsync(int id);
    }
}
