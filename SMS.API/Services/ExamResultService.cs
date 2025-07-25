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
    public class ExamResultService : IExamResultService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ExamResultService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ExamResultCreateDto> CreateExamResultAsync(ExamResultCreateDto examResultCreateDto)
        {
            var newExamResult = new ExamResult
            {
                StudentId = examResultCreateDto.StudentId,
                ExamId = examResultCreateDto.ExamId,
                SubjectId = examResultCreateDto.SubjectId,
                MarksObtained = examResultCreateDto.MarksObtained,
                Grade = examResultCreateDto.Grade,
                Remarks = examResultCreateDto.Remarks,
                RecordedBy = examResultCreateDto.RecordedBy,
                RecordedAt = DateTime.Now
            };
            _applicationDbContext.ExamResults.Add(newExamResult);
            var result = await _applicationDbContext.SaveChangesAsync();
            return new ExamResultCreateDto
            {
                ResultId = newExamResult.ResultId,
                StudentId = newExamResult.StudentId,
                ExamId = newExamResult.ExamId,
                SubjectId = newExamResult.SubjectId,
                MarksObtained = newExamResult.MarksObtained,
                Grade = newExamResult.Grade,
                Remarks = newExamResult.Remarks,
                RecordedBy = newExamResult.RecordedBy,
                RecordedAt = newExamResult.RecordedAt
            };
        }

        public async Task<bool> DeleteExamResultAsync(int id)
        {
            var examResult = await _applicationDbContext.ExamResults
                .FirstOrDefaultAsync(er => er.ResultId == id);
            if (examResult == null)
            {
                throw new KeyNotFoundException($"Exam result with ID {id} not found.");
            }
            _applicationDbContext.ExamResults.Remove(examResult);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<ExamResultDto>> GetAllExamResultsAsync(int pageNumber, int pageSize)
        {
            var examResults = await _applicationDbContext.ExamResults.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
               .Select(er => new ExamResultDto
               {
                   ResultId = er.ResultId,
                   StudentId = er.StudentId,
                   ExamId = er.ExamId,
                   Grade = er.Grade,
                   MarksObtained = er.MarksObtained,
                   Remarks = er.Remarks,
                   RecordedBy = er.RecordedBy,
                   RecordedAt = er.RecordedAt
               }).OrderByDescending(er => er.ResultId).ToListAsync();
            return examResults;
        }

        public async Task<ExamResultDto> GetExamResultByIdAsync(int id)
        {
            var examResult = await _applicationDbContext.ExamResults
                .Where(er => er.ResultId == id)
                .Select(er => new ExamResultDto
                {
                    ResultId = er.ResultId,
                    StudentId = er.StudentId,
                    ExamId = er.ExamId,
                    SubjectId = er.SubjectId,
                    MarksObtained = er.MarksObtained,
                    Grade = er.Grade,
                    Remarks = er.Remarks,
                    RecordedBy = er.RecordedBy,
                    RecordedAt = er.RecordedAt
                }).FirstOrDefaultAsync();
            if (examResult == null)
            {
                throw new KeyNotFoundException($"Exam result with ID {id} not found.");
            }
            return examResult;
        }

        public async Task<ExamResultUpdateDto> UpdateExamResultAsync(int id, ExamResultUpdateDto examResultUpdateDto)
        {
            var examResult = await _applicationDbContext.ExamResults
                .FirstOrDefaultAsync(er => er.ResultId == id);
            if (examResult == null)
            {
                throw new KeyNotFoundException($"Exam result with ID {id} not found.");
            }
            examResult.StudentId = examResultUpdateDto.StudentId;
            examResult.ExamId = examResultUpdateDto.ExamId;
            examResult.SubjectId = examResultUpdateDto.SubjectId;
            examResult.MarksObtained = examResultUpdateDto.MarksObtained;
            examResult.Grade = examResultUpdateDto.Grade;
            examResult.Remarks = examResultUpdateDto.Remarks;
            examResult.RecordedBy = examResultUpdateDto.RecordedBy;
            _applicationDbContext.ExamResults.Update(examResult);
            await _applicationDbContext.SaveChangesAsync();
            return examResultUpdateDto;
        }
    }
}
