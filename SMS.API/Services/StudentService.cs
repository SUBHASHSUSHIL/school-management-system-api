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
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateStudentDto> CreateStudentAsync(CreateStudentDto createStudent)
        {
            var student = new Domain.Models.Student
            {
                UserId = createStudent.UserId,
                AdmissionNumber = createStudent.AdmissionNumber,
                AdmissionDate = createStudent.AdmissionDate,
                CurrentClassId = createStudent.CurrentClassId,
                CurrentSectionId = createStudent.CurrentSectionId,
                ParentId = createStudent.ParentId,
                BloodGroup = createStudent.BloodGroup,
                MedicalConditions = createStudent.MedicalConditions
            };
            await _applicationDbContext.Students.AddAsync(student);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateStudentDto
            {
                UserId = student.UserId,
                AdmissionNumber = student.AdmissionNumber,
                AdmissionDate = student.AdmissionDate,
                CurrentClassId = student.CurrentClassId,
                CurrentSectionId = student.CurrentSectionId,
                ParentId = student.ParentId,
                BloodGroup = student.BloodGroup,
                MedicalConditions = student.MedicalConditions
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _applicationDbContext.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            _applicationDbContext.Students.Remove(student);
            return true;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize)
        {
            var students = await _applicationDbContext.Students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    UserId = s.UserId,
                    AdmissionNumber = s.AdmissionNumber,
                    AdmissionDate = s.AdmissionDate,
                    CurrentClassId = s.CurrentClassId,
                    CurrentSectionId = s.CurrentSectionId,
                    ParentId = s.ParentId,
                    BloodGroup = s.BloodGroup,
                    MedicalConditions = s.MedicalConditions
                }).OrderByDescending(s => s.StudentId).ToListAsync();
            return students;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _applicationDbContext.Students
                .Where(s => s.StudentId == id)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    UserId = s.UserId,
                    AdmissionNumber = s.AdmissionNumber,
                    AdmissionDate = s.AdmissionDate,
                    CurrentClassId = s.CurrentClassId,
                    CurrentSectionId = s.CurrentSectionId,
                    ParentId = s.ParentId,
                    BloodGroup = s.BloodGroup,
                    MedicalConditions = s.MedicalConditions
                }).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            return student;
        }

        public async Task<UpdateStudentDto> UpdateStudentAsync(int id, UpdateStudentDto updateStudent)
        {
            var student = await _applicationDbContext.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            student.UserId = updateStudent.UserId;
            student.AdmissionNumber = updateStudent.AdmissionNumber;
            student.AdmissionDate = updateStudent.AdmissionDate;
            student.CurrentClassId = updateStudent.CurrentClassId;
            student.CurrentSectionId = updateStudent.CurrentSectionId;
            student.ParentId = updateStudent.ParentId;
            student.BloodGroup = updateStudent.BloodGroup;
            student.MedicalConditions = updateStudent.MedicalConditions;
            _applicationDbContext.Students.Update(student);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateStudentDto
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                AdmissionNumber = student.AdmissionNumber,
                AdmissionDate = student.AdmissionDate,
                CurrentClassId = student.CurrentClassId,
                CurrentSectionId = student.CurrentSectionId,
                ParentId = student.ParentId,
                BloodGroup = student.BloodGroup,
                MedicalConditions = student.MedicalConditions
            };
        }
    }
}
