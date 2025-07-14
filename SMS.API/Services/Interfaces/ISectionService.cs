using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetAllSectionsAsync(int pageNumber, int pageSize);
        Task<SectionDto> GetSectionByIdAsync(int id);
        Task<CreateSectionDto> CreateSectionAsync(CreateSectionDto createSection);
        Task<UpdateSectionDto> UpdateSectionAsync(int id, UpdateSectionDto updateSection);
        Task<bool> DeleteSectionAsync(int id);
    }
}
