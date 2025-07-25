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
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeacherService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateTeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacher)
        {
            var newTeacher = new Teacher
            {
                UserId = createTeacher.UserId,
                EmployeeId = createTeacher.EmployeeId,
                JoiningDate = createTeacher.JoiningDate,
                Qualification = createTeacher.Qualification,
                Specialization = createTeacher.Specialization,
                ExperienceYears = createTeacher.ExperienceYears,
                IsClassTeacher = createTeacher.IsClassTeacher,
                ClassTeacherOfClassId = createTeacher.ClassTeacherOfClassId,
                ClassTeacherOfSectionId = createTeacher.ClassTeacherOfSectionId
            };
            _applicationDbContext.Teachers.Add(newTeacher);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateTeacherDto
            {
                UserId = newTeacher.UserId,
                EmployeeId = newTeacher.EmployeeId,
                JoiningDate = newTeacher.JoiningDate,
                Qualification = newTeacher.Qualification,
                Specialization = newTeacher.Specialization,
                ExperienceYears = newTeacher.ExperienceYears,
                IsClassTeacher = newTeacher.IsClassTeacher,
                ClassTeacherOfClassId = newTeacher.ClassTeacherOfClassId,
                ClassTeacherOfSectionId = newTeacher.ClassTeacherOfSectionId
            };
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _applicationDbContext.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }
            _applicationDbContext.Teachers.Remove(teacher);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync(int pageNumber, int pageSize)
        {
            var teachers = await _applicationDbContext.Teachers.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    UserId = t.UserId,
                    EmployeeId = t.EmployeeId,
                    JoiningDate = t.JoiningDate,
                    Qualification = t.Qualification,
                    Specialization = t.Specialization,
                    ExperienceYears = t.ExperienceYears,
                    IsClassTeacher = t.IsClassTeacher,
                    ClassTeacherOfClassId = t.ClassTeacherOfClassId,
                    ClassTeacherOfSectionId = t.ClassTeacherOfSectionId
                }).OrderByDescending(t => t.TeacherId).ToListAsync();
            return teachers;
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _applicationDbContext.Teachers
                .Where(t => t.TeacherId == id)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    UserId = t.UserId,
                    EmployeeId = t.EmployeeId,
                    JoiningDate = t.JoiningDate,
                    Qualification = t.Qualification,
                    Specialization = t.Specialization,
                    ExperienceYears = t.ExperienceYears,
                    IsClassTeacher = t.IsClassTeacher,
                    ClassTeacherOfClassId = t.ClassTeacherOfClassId,
                    ClassTeacherOfSectionId = t.ClassTeacherOfSectionId
                }).FirstOrDefaultAsync();
            return teacher;
        }

        public async Task<UpdateTeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto updateTeacher)
        {
            var existingTeacher = await _applicationDbContext.Teachers.FindAsync(id);
            if (existingTeacher == null)
            {
                return null;
            }
            existingTeacher.UserId = updateTeacher.UserId;
            existingTeacher.EmployeeId = updateTeacher.EmployeeId;
            existingTeacher.JoiningDate = updateTeacher.JoiningDate;
            existingTeacher.Qualification = updateTeacher.Qualification;
            existingTeacher.Specialization = updateTeacher.Specialization;
            existingTeacher.ExperienceYears = updateTeacher.ExperienceYears;
            existingTeacher.IsClassTeacher = updateTeacher.IsClassTeacher;
            existingTeacher.ClassTeacherOfClassId = updateTeacher.ClassTeacherOfClassId;
            existingTeacher.ClassTeacherOfSectionId = updateTeacher.ClassTeacherOfSectionId;
            _applicationDbContext.Teachers.Update(existingTeacher);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateTeacherDto
            {
                TeacherId = existingTeacher.TeacherId,
                UserId = existingTeacher.UserId,
                EmployeeId = existingTeacher.EmployeeId,
                JoiningDate = existingTeacher.JoiningDate,
                Qualification = existingTeacher.Qualification,
                Specialization = existingTeacher.Specialization,
                ExperienceYears = existingTeacher.ExperienceYears,
                IsClassTeacher = existingTeacher.IsClassTeacher,
                ClassTeacherOfClassId = existingTeacher.ClassTeacherOfClassId,
                ClassTeacherOfSectionId = existingTeacher.ClassTeacherOfSectionId
            };
        }
    }
}
