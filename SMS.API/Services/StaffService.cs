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
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StaffService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateStaffDto> CreateStaffAsync(CreateStaffDto createStaff)
        {
            var newStaff = new Domain.Models.Staff
            {
                UserId = createStaff.UserId,
                EmployeeId = createStaff.EmployeeId,
                JoiningDate = createStaff.JoiningDate,
                Position = createStaff.Position,
                Department = createStaff.Department
            };
            await _applicationDbContext.Staffs.AddAsync(newStaff);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateStaffDto
            {
                UserId = newStaff.UserId,
                EmployeeId = newStaff.EmployeeId,
                JoiningDate = newStaff.JoiningDate,
                Position = newStaff.Position,
                Department = newStaff.Department
            };
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var staff = await _applicationDbContext.Staffs.FindAsync(id);
            if (staff == null)
            {
                throw new KeyNotFoundException($"Staff with ID {id} not found.");
            }
            _applicationDbContext.Staffs.Remove(staff);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync(int pageNumber, int pageSize)
        {
            var staffs = await _applicationDbContext.Staffs.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StaffDto
                {
                    StaffId = s.StaffId,
                    UserId = s.UserId,
                    EmployeeId = s.EmployeeId,
                    JoiningDate = s.JoiningDate,
                    Position = s.Position,
                    Department = s.Department
                }).OrderByDescending(s => s.StaffId).ToListAsync();
            return staffs;
        }

        public async Task<StaffDto> GetStaffByIdAsync(int id)
        {
            var staff = await _applicationDbContext
                .Staffs
                .Where(s => s.StaffId == id)
                .Select(s => new StaffDto
                {
                    StaffId = s.StaffId,
                    UserId = s.UserId,
                    EmployeeId = s.EmployeeId,
                    JoiningDate = s.JoiningDate,
                    Position = s.Position,
                    Department = s.Department
                }).FirstOrDefaultAsync();
            if (staff == null)
            {
                throw new KeyNotFoundException($"Staff with ID {id} not found.");
            }
            return staff;
        }

        public async Task<UpdateStaffDto> UpdateStaffAsync(int id, UpdateStaffDto updateStaff)
        {
            var staff = await _applicationDbContext.Staffs.FindAsync(id);
            if (staff == null)
            {
                throw new KeyNotFoundException($"Staff with ID {id} not found.");
            }
            staff.UserId = updateStaff.UserId;
            staff.EmployeeId = updateStaff.EmployeeId;
            staff.JoiningDate = updateStaff.JoiningDate;
            staff.Position = updateStaff.Position;
            staff.Department = updateStaff.Department;
            _applicationDbContext.Staffs.Update(staff);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateStaffDto
            {
                StaffId = staff.StaffId,
                UserId = staff.UserId,
                EmployeeId = staff.EmployeeId,
                JoiningDate = staff.JoiningDate,
                Position = staff.Position,
                Department = staff.Department
            };
        }
    }
}
