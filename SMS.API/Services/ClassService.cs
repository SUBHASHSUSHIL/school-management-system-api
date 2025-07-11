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
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ClassService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateClassDto> CreateClassAsync(CreateClassDto createClassDto)
        {
            var newClass = new Class
            {
                ClassName = createClassDto.ClassName,
                ClassNumeric = createClassDto.ClassNumeric,
                TeacherInChargeId = createClassDto.TeacherInChargeId,
                Description = createClassDto.Description
            };

            _applicationDbContext.Classes.Add(newClass);
            await _applicationDbContext.SaveChangesAsync();
            return createClassDto;
        }

        public async Task<bool> DeleteClassAsync(int classId)
        {
            var classEntity = await _applicationDbContext.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId);
            if (classEntity == null)
            {
                throw new Exception($"Class with ID {classId} not found.");
            }
            _applicationDbContext.Classes.Remove(classEntity);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<ClassDto> GetClassByIdAsync(int classId)
        {
            var classEntity = await _applicationDbContext.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId);
            if (classEntity == null)
            {
                throw new Exception($"Class with ID {classId} not found.");
            }
            return new ClassDto
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Description = classEntity.Description,
                TeacherInChargeId = classEntity.TeacherInChargeId,
                ClassNumeric = classEntity.ClassNumeric
            };
        }

        public async Task<List<ClassDto>> GetClassesAsync(int pageNumber, int pageSize)
        {
            var classes = await _applicationDbContext.Classes.OrderByDescending(x => x.ClassId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return classes.Select(c => new ClassDto
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName,
                Description = c.Description,
                TeacherInChargeId = c.TeacherInChargeId,
                ClassNumeric = c.ClassNumeric
            }).ToList();
        }

        public async Task<bool> UpdateClassAsync(int classId, CreateClassDto updateClassDto)
        {
            var classEntity = await _applicationDbContext.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId);
            if (classEntity == null)
            {
                throw new Exception($"Class with ID {classId} not found.");
            }

            classEntity.ClassName = updateClassDto.ClassName;
            classEntity.ClassNumeric = updateClassDto.ClassNumeric;
            classEntity.TeacherInChargeId = updateClassDto.TeacherInChargeId;
            classEntity.Description = updateClassDto.Description;

            _applicationDbContext.Classes.Update(classEntity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
