using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface INoticeService
    {
        Task<IEnumerable<NoticeDto>> GetAllNoticesAsync(int pageNumber, int pageSize);
        Task<NoticeDto> GetNoticeByIdAsync(int id);
        Task<CreateNoticeDto> CreateNoticeAsync(CreateNoticeDto createNotice);
        Task<UpdateNoticeDto> UpdateNoticeAsync(int id, UpdateNoticeDto updateNotice);
        Task<bool> DeleteNoticeAsync(int id);
    }
}
