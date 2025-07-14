using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllStaffAsync(int pageNumber, int pageSize);
        Task<StaffDto> GetStaffByIdAsync(int id);
        Task<CreateStaffDto> CreateStaffAsync(CreateStaffDto createStaff);
        Task<UpdateStaffDto> UpdateStaffAsync(int id, UpdateStaffDto updateStaff);
        Task<bool> DeleteStaffAsync(int id);
    }
}
