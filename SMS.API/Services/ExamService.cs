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
    public class ExamService : IExamService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ExamService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateExamDto> CreateExamAsync(CreateExamDto createExamDto)
        {
            var newExam = new Exam
            {
                ExamName = createExamDto.ExamName,
                AcademicYearId = createExamDto.AcademicYearId,
                StartDate = createExamDto.StartDate,
                EndDate = createExamDto.EndDate,
                Description = createExamDto.Description,
                IsPublished = createExamDto.IsPublished
            };
            _applicationDbContext.Exams.Add(newExam);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateExamDto
            {
                ExamName = newExam.ExamName,
                AcademicYearId = newExam.AcademicYearId,
                StartDate = newExam.StartDate,
                EndDate = newExam.EndDate,
                Description = newExam.Description,
                IsPublished = newExam.IsPublished
            };
        }

        public async Task<bool> DeleteExamAsync(int id)
        {
            var examEntity = await _applicationDbContext.Exams.FindAsync(id);
            if (examEntity == null)
            {
                return false;
            }
            _applicationDbContext.Exams.Remove(examEntity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ExamDto>> GetAllExamAsync(int pageNumber, int pageSize)
        {
            var exams = await _applicationDbContext.Exams.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new ExamDto
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName,
                    AcademicYearId=e.AcademicYearId,
                    StartDate=e.StartDate,
                    EndDate=e.EndDate,
                    Description = e.Description,
                    IsPublished=e.IsPublished
                }).OrderByDescending(e => e.ExamId).ToListAsync();
            return exams;
        }

        public async Task<ExamDto> GetExamByIdAsync(int id)
        {
            var examEntity = await _applicationDbContext.Exams
                .Where(e => e.ExamId == id)
                .Select(e => new ExamDto
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName,
                    AcademicYearId = e.AcademicYearId,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Description = e.Description,
                    IsPublished=e.IsPublished
                })
                .FirstOrDefaultAsync();

            return examEntity;
        }

        public async Task<CreateExamDto> UpdateExamAsync(int id, CreateExamDto updateExamDto)
        {
            var getExam = await _applicationDbContext.Exams.FindAsync(id);
            if (getExam == null)
            {
                throw new KeyNotFoundException($"Event with Id {id} not found.");
            }
            getExam.ExamName = updateExamDto.ExamName;
            getExam.AcademicYearId = updateExamDto.AcademicYearId;
            getExam.StartDate = updateExamDto.StartDate;
            getExam.EndDate = updateExamDto.EndDate;
            getExam.Description = updateExamDto.Description;
            getExam.IsPublished = updateExamDto.IsPublished;
            _applicationDbContext.Exams.Update(getExam);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateExamDto
            {
               ExamName = getExam.ExamName,
               AcademicYearId = getExam.AcademicYearId,
               StartDate = getExam.StartDate,
               EndDate = getExam.EndDate,
               Description = updateExamDto.Description,
               IsPublished = updateExamDto.IsPublished
            };
        }
    }
}
