using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public AttendanceService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Attendance> CreateAttendanceAsync(Attendance attendance)
        {
            var newAttendance = new Attendance
            {
                StudentId = attendance.StudentId,
                ClassId = attendance.ClassId,
                SectionId = attendance.SectionId,
                Date = attendance.Date,
                Status = attendance.Status,
                Remarks = attendance.Remarks,
                RecordedBy = attendance.RecordedBy
            };
            _applicationDbContext.Attendances.Add(newAttendance);
            await _applicationDbContext.SaveChangesAsync();
            return newAttendance;
        }

        public async Task<bool> DeleteAttendanceAsync(int id)
        {
            var attendance = await _applicationDbContext.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id);
            if (attendance == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }
            else
            {
                _applicationDbContext.Attendances.Remove(attendance);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Attendance>> GetAttendanceAsync(int pageNumber, int pageSize)
        {
            var attendances = await _applicationDbContext.Attendances.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderByDescending(x => x.Date)
                .ToListAsync();
            return attendances;
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int id)
        {
            var attendance = _applicationDbContext.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id);

            if (attendance == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }
            else
            {
                return await attendance;
            }
        }

        public async Task<Attendance> UpdateAttendanceAsync(int id, Attendance attendance)
        {
            var existingAttendance = await _applicationDbContext.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id);
            if (existingAttendance == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }
            else
            {
                existingAttendance.StudentId = attendance.StudentId;
                existingAttendance.ClassId = attendance.ClassId;
                existingAttendance.SectionId = attendance.SectionId;
                existingAttendance.Date = attendance.Date;
                existingAttendance.Status = attendance.Status;
                existingAttendance.Remarks = attendance.Remarks;
                existingAttendance.RecordedBy = attendance.RecordedBy;
                _applicationDbContext.Attendances.Update(existingAttendance);
                await _applicationDbContext.SaveChangesAsync();
                return existingAttendance;
            }
        }
    }
}
