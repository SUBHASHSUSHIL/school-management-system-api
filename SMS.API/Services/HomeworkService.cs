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
    public class HomeworkService : IHomeworkService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeworkService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateHomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto)
        {
            var newhomework = new Homework
            {
                ClassId = homeworkDto.ClassId,
                SectionId = homeworkDto.SectionId,
                SubjectId = homeworkDto.SubjectId,
                TeacherId = homeworkDto.TeacherId,
                Title = homeworkDto.Title,
                Description = homeworkDto.Description,
                AssignedDate = homeworkDto.AssignedDate,
                DueDate = homeworkDto.DueDate,
                MaxMarks = homeworkDto.MaxMarks,
                CreatedAt = DateTime.UtcNow
            };
            _applicationDbContext.Homeworks.Add(newhomework);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateHomeworkDto
            {
                ClassId = newhomework.ClassId,
                SectionId = newhomework.SectionId,
                SubjectId = newhomework.SubjectId,
                TeacherId = newhomework.TeacherId,
                Title = newhomework.Title,
                Description = newhomework.Description,
                AssignedDate = newhomework.AssignedDate,
                DueDate = newhomework.DueDate,
                MaxMarks = newhomework.MaxMarks,
                CreatedAt = newhomework.CreatedAt
            };
        }

        public async Task<bool> DeleteHomeworkAsync(int id)
        {
            var homework = await _applicationDbContext.Homeworks.FindAsync(id);
            if (homework == null)
            {
                throw new KeyNotFoundException($"Homework with ID {id} not found.");
            }
            _applicationDbContext.Homeworks.Remove(homework);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<HomeworkDto>> GetAllHomeworksAsync(int pageNumber = 1, int pageSize = 10)
        {
            var homeworks = _applicationDbContext.Homeworks
                .Skip((pageNumber - 1) * pageSize).OrderByDescending(h => h.HomeworkId)
                .Take(pageSize)
                .Select(h => new HomeworkDto
                {
                    HomeworkId = h.HomeworkId,
                    ClassId = h.ClassId,
                    SectionId = h.SectionId,
                    SubjectId = h.SubjectId,
                    TeacherId = h.TeacherId,
                    Title = h.Title,
                    Description = h.Description,
                    AssignedDate = h.AssignedDate,
                    DueDate = h.DueDate,
                    MaxMarks = h.MaxMarks,
                    CreatedAt = h.CreatedAt
                }).ToListAsync();
            return await homeworks;
        }

        public async Task<HomeworkDto> GetHomeworkByIdAsync(int id)
        {
            var homework = await _applicationDbContext.Homeworks
                .Where(h => h.HomeworkId == id)
                .Select(h => new HomeworkDto
                {
                    HomeworkId = h.HomeworkId,
                    ClassId = h.ClassId,
                    SectionId = h.SectionId,
                    SubjectId = h.SubjectId,
                    TeacherId = h.TeacherId,
                    Title = h.Title,
                    Description = h.Description,
                    AssignedDate = h.AssignedDate,
                    DueDate = h.DueDate,
                    MaxMarks = h.MaxMarks,
                    CreatedAt = h.CreatedAt
                }).FirstOrDefaultAsync();
            if (homework == null)
            {
                throw new KeyNotFoundException($"Homework with ID {id} not found.");
            }
            return homework;
        }

        public async Task<UpdateHomeworkDto> UpdateHomeworkAsync(int id, UpdateHomeworkDto homeworkDto)
        {
            var homework = await _applicationDbContext.Homeworks.FindAsync(id);
            if (homework == null)
            {
                throw new KeyNotFoundException($"Homework with ID {id} not found.");
            }
            homework.ClassId = homeworkDto.ClassId;
            homework.SectionId = homeworkDto.SectionId;
            homework.SubjectId = homeworkDto.SubjectId;
            homework.TeacherId = homeworkDto.TeacherId;
            homework.Title = homeworkDto.Title;
            homework.Description = homeworkDto.Description;
            homework.AssignedDate = homeworkDto.AssignedDate;
            homework.DueDate = homeworkDto.DueDate;
            homework.MaxMarks = homeworkDto.MaxMarks;
            homework.CreatedAt = homeworkDto.CreatedAt;
            _applicationDbContext.Homeworks.Update(homework);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateHomeworkDto
            {
                ClassId = homework.ClassId,
                SectionId = homework.SectionId,
                SubjectId = homework.SubjectId,
                TeacherId = homework.TeacherId,
                Title = homework.Title,
                Description = homework.Description,
                AssignedDate = homework.AssignedDate,
                DueDate = homework.DueDate,
                MaxMarks = homework.MaxMarks,
                CreatedAt = homework.CreatedAt
            };
        }
    }
}
