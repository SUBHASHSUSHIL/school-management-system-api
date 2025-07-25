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
    public class StudentHomeworkService : IStudentHomeworkService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentHomeworkService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateStudentHomeworkDto> CreateStudentHomeworkAsync(CreateStudentHomeworkDto createStudentHomework)
        {
            
            var homeworkEntity = new StudentHomework
            {
                HomeworkId = createStudentHomework.HomeworkId,
                StudentId = createStudentHomework.StudentId,
                SubmissionDate = DateTime.UtcNow,
                MarksObtained = createStudentHomework.MarksObtained,
                Feedback = createStudentHomework.Feedback,
                Status = createStudentHomework.Status
            };

            _applicationDbContext.StudentHomeworks.Add(homeworkEntity);
            await _applicationDbContext.SaveChangesAsync();

            return new CreateStudentHomeworkDto
            {
                StudentHomeworkId = homeworkEntity.StudentHomeworkId,
                HomeworkId = homeworkEntity.HomeworkId,
                StudentId = homeworkEntity.StudentId,
                SubmissionDate = homeworkEntity.SubmissionDate,
                MarksObtained = homeworkEntity.MarksObtained,
                Feedback = homeworkEntity.Feedback,
                Status = homeworkEntity.Status
            };
        }

        public async Task<bool> DeleteStudentHomeworkAsync(int id)
        {
            var studentHomework = await _applicationDbContext.StudentHomeworks.FindAsync(id);
            if (studentHomework == null)
            {
                throw new KeyNotFoundException($"Student homework with ID {id} not found.");
            }
            _applicationDbContext.StudentHomeworks.Remove(studentHomework);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StudentHomeworkDto>> GetAllStudentHomeworksAsync(int pageNumber, int pageSize)
        {
            var studentHomeworks = await _applicationDbContext.StudentHomeworks.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(sh => new StudentHomeworkDto
                {
                    StudentHomeworkId = sh.StudentHomeworkId,
                    HomeworkId = sh.HomeworkId,
                    StudentId = sh.StudentId,
                    SubmissionDate = sh.SubmissionDate,
                    MarksObtained = sh.MarksObtained,
                    Feedback = sh.Feedback,
                    Status = sh.Status
                }).OrderByDescending(sh => sh.SubmissionDate).ToListAsync();
            return studentHomeworks;
        }

        public async Task<StudentHomeworkDto> GetStudentHomeworkByIdAsync(int id)
        {
            var studentHomework = await _applicationDbContext.StudentHomeworks
                .Where(sh => sh.StudentHomeworkId == id)
                .Select(sh => new StudentHomeworkDto
                {
                    StudentHomeworkId = sh.StudentHomeworkId,
                    HomeworkId = sh.HomeworkId,
                    StudentId = sh.StudentId,
                    SubmissionDate = sh.SubmissionDate,
                    MarksObtained = sh.MarksObtained,
                    Feedback = sh.Feedback,
                    Status = sh.Status
                }).FirstOrDefaultAsync();
            if (studentHomework == null)
            {
                throw new KeyNotFoundException($"Student homework with ID {id} not found.");
            }
            return studentHomework;
        }

        public async Task<UpdateStudentHomeworkDto> UpdateStudentHomeworkAsync(int id, UpdateStudentHomeworkDto updateStudentHomework)
        {
            var studentHomework = await _applicationDbContext.StudentHomeworks.FindAsync(id);
            if (studentHomework == null)
            {
                throw new KeyNotFoundException($"Student homework with ID {id} not found.");
            }
            studentHomework.HomeworkId = updateStudentHomework.HomeworkId;
            studentHomework.StudentId = updateStudentHomework.StudentId;
            studentHomework.SubmissionDate = updateStudentHomework.SubmissionDate;
            studentHomework.MarksObtained = updateStudentHomework.MarksObtained;
            studentHomework.Feedback = updateStudentHomework.Feedback;
            studentHomework.Status = updateStudentHomework.Status;
            _applicationDbContext.StudentHomeworks.Update(studentHomework);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateStudentHomeworkDto
            {
                StudentHomeworkId = studentHomework.StudentHomeworkId,
                HomeworkId = studentHomework.HomeworkId,
                StudentId = studentHomework.StudentId,
                SubmissionDate = studentHomework.SubmissionDate,
                MarksObtained = studentHomework.MarksObtained,
                Feedback = studentHomework.Feedback,
                Status = studentHomework.Status
            };
        }
    }
}
