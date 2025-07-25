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
    public class ExamScheduleService : IExamScheduleService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ExamScheduleService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateExamScheduleDto> CreateExamScheduleAsync(CreateExamScheduleDto createExamScheduleDto)
        {
            var newExamSchedule = new Domain.Models.ExamSchedule
            {
                ExamId = createExamScheduleDto.ExamId,
                SubjectId = createExamScheduleDto.SubjectId,
                ClassId = createExamScheduleDto.ClassId,
                ExamDate = createExamScheduleDto.ExamDate,
                StartTime = createExamScheduleDto.StartTime,
                EndTime = createExamScheduleDto.EndTime,
                MaxMarks = createExamScheduleDto.MaxMarks,
                PassingMarks = createExamScheduleDto.PassingMarks,
                RoomNumber = createExamScheduleDto.RoomNumber
            };
            _applicationDbContext.ExamSchedules.Add(newExamSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateExamScheduleDto
            {
                ExamId = newExamSchedule.ExamId,
                SubjectId = newExamSchedule.SubjectId,
                ClassId = newExamSchedule.ClassId,
                ExamDate = newExamSchedule.ExamDate,
                StartTime = newExamSchedule.StartTime,
                EndTime = newExamSchedule.EndTime,
                MaxMarks = newExamSchedule.MaxMarks,
                PassingMarks = newExamSchedule.PassingMarks,
                RoomNumber = newExamSchedule.RoomNumber
            };
        }

        public async Task<bool> DeleteExamScheduleAsync(int scheduleId)
        {
            var examSchedule = await _applicationDbContext.ExamSchedules.FindAsync(scheduleId);
            if (examSchedule == null)
            {
                throw new KeyNotFoundException($"Exam schedule with ID {scheduleId} not found.");
            }
            _applicationDbContext.ExamSchedules.Remove(examSchedule);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ExamScheduleDto>> GetAllExamSchedulesAsync(int pageNumber, int pageSize)
        {
            var examSchedules = await _applicationDbContext.ExamSchedules.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(es => new ExamScheduleDto
                {
                    ScheduleId = es.ScheduleId,
                    ExamId = es.ExamId,
                    SubjectId = es.SubjectId,
                    ClassId = es.ClassId,
                    ExamDate = es.ExamDate,
                    StartTime = es.StartTime,
                    EndTime = es.EndTime,
                    MaxMarks = es.MaxMarks,
                    PassingMarks = es.PassingMarks,
                    RoomNumber = es.RoomNumber
                }).OrderByDescending(x => x.ScheduleId).ToListAsync();
            return examSchedules;
        }

        public async Task<ExamScheduleDto> GetExamScheduleByIdAsync(int scheduleId)
        {
            var examSchedule = await _applicationDbContext.ExamSchedules
                .Where(es => es.ScheduleId == scheduleId)
                .Select(es => new ExamScheduleDto
                {
                    ScheduleId = es.ScheduleId,
                    ExamId = es.ExamId,
                    SubjectId = es.SubjectId,
                    ClassId = es.ClassId,
                    ExamDate = es.ExamDate,
                    StartTime = es.StartTime,
                    EndTime = es.EndTime,
                    MaxMarks = es.MaxMarks,
                    PassingMarks = es.PassingMarks,
                    RoomNumber = es.RoomNumber
                }).FirstOrDefaultAsync();

            if (examSchedule == null)
            {
                throw new KeyNotFoundException($"Exam schedule with ID {scheduleId} not found.");
            }
            return examSchedule;
        }

        public async Task<UpdateExamScheduleDto> UpdateExamScheduleAsync(int scheduleId, UpdateExamScheduleDto updateExamScheduleDto)
        {
            var examSchedule = await _applicationDbContext.ExamSchedules.FindAsync(scheduleId);
            if (examSchedule == null)
            {
                throw new KeyNotFoundException($"Exam schedule with ID {scheduleId} not found.");
            }
            examSchedule.ExamId = updateExamScheduleDto.ExamId;
            examSchedule.SubjectId = updateExamScheduleDto.SubjectId;
            examSchedule.ClassId = updateExamScheduleDto.ClassId;
            examSchedule.ExamDate = updateExamScheduleDto.ExamDate;
            examSchedule.StartTime = updateExamScheduleDto.StartTime;
            examSchedule.EndTime = updateExamScheduleDto.EndTime;
            examSchedule.MaxMarks = updateExamScheduleDto.MaxMarks;
            examSchedule.PassingMarks = updateExamScheduleDto.PassingMarks;
            examSchedule.RoomNumber = updateExamScheduleDto.RoomNumber;
            _applicationDbContext.ExamSchedules.Update(examSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return updateExamScheduleDto;
        }
    }
}
