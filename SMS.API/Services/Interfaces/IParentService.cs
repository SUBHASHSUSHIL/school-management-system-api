using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IParentService
    {
        Task<IEnumerable<ParentDto>> GetAllParentsAsync(int pageNumber, int pageSize);
        Task<ParentDto> GetParentByIdAsync(int id);
        Task<CreateParentDto> CreateParentAsync(CreateParentDto createParent);
        Task<UpdateParentDto> UpdateParentAsync(int id, UpdateParentDto updateParent);
        Task<bool> DeleteParentAsync(int id);
    }
}
