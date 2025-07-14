using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubjectService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateSubjectDto> CreateSubjectAsync(CreateSubjectDto createSubject)
        {
            var newSubject = new Domain.Models.Subject
            {
                SubjectName = createSubject.SubjectName,
                SubjectCode = createSubject.SubjectCode,
                Description = createSubject.Description,
                IsCore = createSubject.IsCore
            };
            _applicationDbContext.Subjects.Add(newSubject);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateSubjectDto
            {
                SubjectId = newSubject.SubjectId,
                SubjectName = newSubject.SubjectName,
                SubjectCode = newSubject.SubjectCode,
                Description = newSubject.Description,
                IsCore = newSubject.IsCore
            };
        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var subject = await _applicationDbContext.Subjects.FindAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found.");
            }
            _applicationDbContext.Subjects.Remove(subject);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync(int pageNumber, int pageSize)
        {
            var subjects = await _applicationDbContext.Subjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SubjectDto
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.SubjectName,
                    SubjectCode = s.SubjectCode,
                    Description = s.Description,
                    IsCore = s.IsCore
                }).OrderByDescending(s => s.SubjectId).ToListAsync();
            return subjects;
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(int id)
        {
            var subject = await _applicationDbContext.Subjects
                .Where(s => s.SubjectId == id)
                .Select(s => new SubjectDto
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.SubjectName,
                    SubjectCode = s.SubjectCode,
                    Description = s.Description,
                    IsCore = s.IsCore
                }).FirstOrDefaultAsync();
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found.");
            }
            return subject;
        }

        public async Task<UpdateSubjectDto> UpdateSubjectAsync(int id, UpdateSubjectDto updateSubject)
        {
            var subject = await _applicationDbContext.Subjects.FindAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found.");
            }
            subject.SubjectName = updateSubject.SubjectName;
            subject.SubjectCode = updateSubject.SubjectCode;
            subject.Description = updateSubject.Description;
            subject.IsCore = updateSubject.IsCore;
            _applicationDbContext.Subjects.Update(subject);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateSubjectDto
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                SubjectCode = subject.SubjectCode,
                Description = subject.Description,
                IsCore = subject.IsCore
            };
        }
    }
}
