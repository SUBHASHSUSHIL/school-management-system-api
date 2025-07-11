using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAttendanceAsync(int pageNumber, int pageSize);
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task<Attendance> CreateAttendanceAsync(Attendance attendance);
        Task<Attendance> UpdateAttendanceAsync(int id, Attendance attendance);
        Task<bool> DeleteAttendanceAsync(int id);
    }
}
