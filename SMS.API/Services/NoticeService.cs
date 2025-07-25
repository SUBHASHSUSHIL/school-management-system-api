using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class NoticeService : INoticeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NoticeService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateNoticeDto> CreateNoticeAsync(CreateNoticeDto createNotice)
        {
            var newNotice = new Notice
            {
                Title = createNotice.Title,
                Content = createNotice.Content,
                PublishDate = createNotice.PublishDate,
                ExpireDate = createNotice.ExpireDate,
                CreatedBy = createNotice.CreatedBy,
                TargetAudience = createNotice.TargetAudience,
                IsImportant = createNotice.IsImportant
            };
            _applicationDbContext.Notices.Add(newNotice);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateNoticeDto
            {
                Title = newNotice.Title,
                Content = newNotice.Content,
                PublishDate = newNotice.PublishDate,
                ExpireDate = newNotice.ExpireDate,
                CreatedBy = newNotice.CreatedBy,
                TargetAudience = newNotice.TargetAudience,
                IsImportant = newNotice.IsImportant
            };
        }

        public async Task<bool> DeleteNoticeAsync(int id)
        {
            var notice = await _applicationDbContext.Notices.FindAsync(id);
            if (notice == null)
            {
                throw new KeyNotFoundException($"Notice with ID {id} not found.");
            }
            _applicationDbContext.Notices.Remove(notice);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<NoticeDto>> GetAllNoticesAsync(int pageNumber, int pageSize)
        {
            var notices = await _applicationDbContext.Notices.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NoticeDto
                {
                    NoticeId = n.NoticeId,
                    Title = n.Title,
                    Content = n.Content,
                    PublishDate = n.PublishDate,
                    ExpireDate = n.ExpireDate,
                    CreatedBy = n.CreatedBy,
                    TargetAudience = n.TargetAudience,
                    IsImportant = n.IsImportant
                }).OrderByDescending(x => x.NoticeId).ToListAsync();
            return notices;
        }

        public async Task<NoticeDto> GetNoticeByIdAsync(int id)
        {
            var notice = await _applicationDbContext.Notices
                .Where(n => n.NoticeId == id)
                .Select(n => new NoticeDto
                {
                    NoticeId = n.NoticeId,
                    Title = n.Title,
                    Content = n.Content,
                    PublishDate = n.PublishDate,
                    ExpireDate = n.ExpireDate,
                    CreatedBy = n.CreatedBy,
                    TargetAudience = n.TargetAudience,
                    IsImportant = n.IsImportant
                }).FirstOrDefaultAsync();
            if (notice == null)
            {
                throw new KeyNotFoundException($"Notice with ID {id} not found.");
            }
            return notice;
        }

        public async Task<UpdateNoticeDto> UpdateNoticeAsync(int id, UpdateNoticeDto updateNotice)
        {
            var existingNotice = await _applicationDbContext.Notices.FindAsync(id);
            if (existingNotice == null)
            {
                throw new KeyNotFoundException($"Notice with ID {id} not found.");
            }
            existingNotice.Title = updateNotice.Title;
            existingNotice.Content = updateNotice.Content;
            existingNotice.PublishDate = updateNotice.PublishDate;
            existingNotice.ExpireDate = updateNotice.ExpireDate;
            existingNotice.CreatedBy = updateNotice.CreatedBy;
            existingNotice.TargetAudience = updateNotice.TargetAudience;
            existingNotice.IsImportant = updateNotice.IsImportant;
            _applicationDbContext.Notices.Update(existingNotice);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateNoticeDto
            {
                NoticeId = existingNotice.NoticeId,
                Title = existingNotice.Title,
                Content = existingNotice.Content,
                PublishDate = existingNotice.PublishDate,
                ExpireDate = existingNotice.ExpireDate,
                CreatedBy = existingNotice.CreatedBy,
                TargetAudience = existingNotice.TargetAudience,
                IsImportant = existingNotice.IsImportant
            };
        }
    }
}
