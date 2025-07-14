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
    public class TimetableService : ITimetableService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TimetableService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateTimetableDto> CreateTimetableAsync(CreateTimetableDto createTimetable)
        {
            var timetable = new Timetable
            {
                ClassId = createTimetable.ClassId,
                SectionId = createTimetable.SectionId,
                SubjectId = createTimetable.SubjectId,
                TeacherId = createTimetable.TeacherId,
                DayOfWeek = createTimetable.DayOfWeek,
                PeriodNumber = createTimetable.PeriodNumber,
                StartTime = createTimetable.StartTime,
                EndTime = createTimetable.EndTime,
                RoomNumber = createTimetable.RoomNumber
            };
            _applicationDbContext.Timetables.Add(timetable);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateTimetableDto
            {
                ClassId = timetable.ClassId,
                SectionId = timetable.SectionId,
                SubjectId = timetable.SubjectId,
                TeacherId = timetable.TeacherId,
                DayOfWeek = timetable.DayOfWeek,
                PeriodNumber = timetable.PeriodNumber,
                StartTime = timetable.StartTime,
                EndTime = timetable.EndTime,
                RoomNumber = timetable.RoomNumber
            };
        }

        public async Task<bool> DeleteTimetableAsync(int id)
        {
            var timetable = await _applicationDbContext.Timetables.FindAsync(id);
            if (timetable == null)
            {
                throw new KeyNotFoundException($"Timetable with ID {id} not found.");
            }
            _applicationDbContext.Timetables.Remove(timetable);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TimetableDto>> GetAllTimetablesAsync(int pageNumber, int pageSize)
        {
            var timetables = await _applicationDbContext.Timetables
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TimetableDto
                {
                    TimetableId = t.TimetableId,
                    ClassId = t.ClassId,
                    SectionId = t.SectionId,
                    SubjectId = t.SubjectId,
                    TeacherId = t.TeacherId,
                    DayOfWeek = t.DayOfWeek,
                    PeriodNumber = t.PeriodNumber,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    RoomNumber = t.RoomNumber,
                    IsActive = t.IsActive
                }).OrderByDescending(t => t.TimetableId).ToListAsync();
            return timetables;
        }

        public async Task<TimetableDto> GetTimetableByIdAsync(int id)
        {
            var timetable = await _applicationDbContext.Timetables
                .Where(t => t.TimetableId == id)
                .Select(t => new TimetableDto
                {
                    TimetableId = t.TimetableId,
                    ClassId = t.ClassId,
                    SectionId = t.SectionId,
                    SubjectId = t.SubjectId,
                    TeacherId = t.TeacherId,
                    DayOfWeek = t.DayOfWeek,
                    PeriodNumber = t.PeriodNumber,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    RoomNumber = t.RoomNumber,
                    IsActive = t.IsActive
                }).FirstOrDefaultAsync();
            if (timetable == null)
            {
                throw new KeyNotFoundException($"Timetable with ID {id} not found.");
            }
            return timetable;
        }

        public async Task<UpdateTimetableDto> UpdateTimetableAsync(int id, UpdateTimetableDto updateTimetable)
        {
            var timetable = await _applicationDbContext.Timetables.FindAsync(id);
            if (timetable == null)
            {
                throw new KeyNotFoundException($"Timetable with ID {id} not found.");
            }
            timetable.ClassId = updateTimetable.ClassId;
            timetable.SectionId = updateTimetable.SectionId;
            timetable.SubjectId = updateTimetable.SubjectId;
            timetable.TeacherId = updateTimetable.TeacherId;
            timetable.DayOfWeek = updateTimetable.DayOfWeek;
            timetable.PeriodNumber = updateTimetable.PeriodNumber;
            timetable.StartTime = updateTimetable.StartTime;
            timetable.EndTime = updateTimetable.EndTime;
            timetable.RoomNumber = updateTimetable.RoomNumber;
            _applicationDbContext.Timetables.Update(timetable);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateTimetableDto
            {
                ClassId = timetable.ClassId,
                SectionId = timetable.SectionId,
                SubjectId = timetable.SubjectId,
                TeacherId = timetable.TeacherId,
                DayOfWeek = timetable.DayOfWeek,
                PeriodNumber = timetable.PeriodNumber,
                StartTime = timetable.StartTime,
                EndTime = timetable.EndTime,
                RoomNumber = timetable.RoomNumber
            };
        }
    }
}
