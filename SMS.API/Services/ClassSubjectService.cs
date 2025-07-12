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
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ClassSubjectService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateClassSubjectDto> CreateClassSubjectAsync(CreateClassSubjectDto classSubjectDto)
        {
            var classSubject = new ClassSubject
            {
                ClassId = classSubjectDto.ClassId,
                SubjectId = classSubjectDto.SubjectId,
                TeacherId = classSubjectDto.TeacherId
            };
            _applicationDbContext.ClassSubjects.Add(classSubject);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateClassSubjectDto
            {
                ClassId = classSubject.ClassId,
                SubjectId = classSubject.SubjectId,
                TeacherId = classSubject.TeacherId
            };
        }

        public async Task<bool> DeleteClassSubjectAsync(int id)
        {
            var classSubject = await _applicationDbContext.ClassSubjects.FindAsync(id);
            if (classSubject == null)
            {
                throw new KeyNotFoundException($"Class subject with ID {id} not found.");
            }
            _applicationDbContext.ClassSubjects.Remove(classSubject);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<ClassSubjectDto> GetClassSubjectByIdAsync(int id)
        {
            var classSubject = await _applicationDbContext.ClassSubjects
                .Where(cs => cs.ClassSubjectId == id)
                .Select(cs => new ClassSubjectDto
                {
                    ClassSubjectId = cs.ClassSubjectId,
                    ClassId = cs.ClassId,
                    SubjectId = cs.SubjectId,
                    TeacherId = cs.TeacherId
                })
                .FirstOrDefaultAsync();
            if (classSubject == null)
            {
                throw new KeyNotFoundException($"Class subject with ID {id} not found.");
            }
            return classSubject;
        }

        public async Task<List<ClassSubjectDto>> GetClassSubjectsAsync(int pageNumber, int pageSize)
        {
            var classSubjects = await _applicationDbContext.ClassSubjects.OrderByDescending(x => x.ClassSubjectId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(cs => new ClassSubjectDto
                {
                    ClassSubjectId = cs.ClassSubjectId,
                    ClassId = cs.ClassId,
                    SubjectId = cs.SubjectId,
                    TeacherId = cs.TeacherId
                })
                .ToListAsync();
            return classSubjects;
        }

        public async Task<ClassSubjectDto> UpdateClassSubjectAsync(int id, CreateClassSubjectDto classSubjectDto)
        {
            var classSubject = await _applicationDbContext.ClassSubjects.FindAsync(id);
            if (classSubject == null)
            {
                throw new KeyNotFoundException($"Class subject with ID {id} not found.");
            }
            classSubject.ClassId = classSubjectDto.ClassId;
            classSubject.SubjectId = classSubjectDto.SubjectId;
            classSubject.TeacherId = classSubjectDto.TeacherId;
            _applicationDbContext.ClassSubjects.Update(classSubject);
            await _applicationDbContext.SaveChangesAsync();
            return new ClassSubjectDto
            {
                ClassSubjectId = classSubject.ClassSubjectId,
                ClassId = classSubject.ClassId,
                SubjectId = classSubject.SubjectId,
                TeacherId = classSubject.TeacherId
            };
        }
    }
}
