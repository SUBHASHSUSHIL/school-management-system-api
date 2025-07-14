using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IFeeStructureService
    {
        Task<IEnumerable<FeeStructureDto>> GetAllFeeStructuresAsync(int pageNumber, int pageSize);
        Task<FeeStructureDto> GetFeeStructureByIdAsync(int id);
        Task<FeeStructureCreateDto> CreateFeeStructureAsync(FeeStructureCreateDto createFeeCategoryDto);
        Task<FeeStructureUpdateDto> UpdateFeeStructureAsync(int id, FeeStructureUpdateDto updateFeeCategoryDto);
        Task<bool> DeleteFeeStructureAsync(int id);
    }
}
