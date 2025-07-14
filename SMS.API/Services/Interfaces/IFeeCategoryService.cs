using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IFeeCategoryService
    {
        Task<IEnumerable<FeeCategoryDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<FeeCategoryDto> GetByIdAsync(int id);
        Task<FeeCategoryDto> CreateAsync(FeeCategoryDto feeCategoryDto);
        Task<FeeCategoryDto> UpdateAsync(int id, FeeCategoryDto feeCategoryDto);
        Task<bool> DeleteAsync(int id);
    }
}
