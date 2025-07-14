using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync(int pageNumber, int pageSize);
        Task<SubjectDto> GetSubjectByIdAsync(int id);
        Task<CreateSubjectDto> CreateSubjectAsync(CreateSubjectDto createSubject);
        Task<UpdateSubjectDto> UpdateSubjectAsync(int id, UpdateSubjectDto updateSubject);
        Task<bool> DeleteSubjectAsync(int id);
    }
}
