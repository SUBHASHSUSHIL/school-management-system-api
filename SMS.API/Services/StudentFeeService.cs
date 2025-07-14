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
    public class StudentFeeService : IStudentFeeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentFeeService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateStudentFeeDto> CreateStudentFeeAsync(CreateStudentFeeDto createStudentFeeDto)
        {
            var newStudentFee = new Domain.Models.StudentFee
            {
                StudentId = createStudentFeeDto.StudentId,
                FeeStructureId = createStudentFeeDto.FeeStructureId,
                Amount = createStudentFeeDto.Amount,
                DueDate = createStudentFeeDto.DueDate,
                PaidAmount = createStudentFeeDto.PaidAmount,
                PaymentStatus = createStudentFeeDto.PaymentStatus
            };
            _applicationDbContext.StudentFees.Add(newStudentFee);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateStudentFeeDto
            {
                StudentId = newStudentFee.StudentId,
                FeeStructureId = newStudentFee.FeeStructureId,
                Amount = newStudentFee.Amount,
                DueDate = newStudentFee.DueDate,
                PaidAmount = newStudentFee.PaidAmount,
                PaymentStatus = newStudentFee.PaymentStatus
            };
        }

        public async Task<bool> DeleteStudentFeeAsync(int studentFeeId)
        {
            var studentFee = await _applicationDbContext.StudentFees
                .FirstOrDefaultAsync(sf => sf.StudentFeeId == studentFeeId);
            if (studentFee == null)
            {
                throw new KeyNotFoundException($"Student fee with ID {studentFeeId} not found.");
            }
            _applicationDbContext.StudentFees.Remove(studentFee);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StudentFeeDto>> GetAllStudentFeesAsync(int pageNumber, int pageSize)
        {
            var studentFees = await _applicationDbContext.StudentFees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(sf => new StudentFeeDto
                {
                    StudentFeeId = sf.StudentFeeId,
                    StudentId = sf.StudentId,
                    FeeStructureId = sf.FeeStructureId,
                    Amount = sf.Amount,
                    DueDate = sf.DueDate,
                    PaidAmount = sf.PaidAmount,
                    PaymentStatus = sf.PaymentStatus
                }).OrderByDescending(sf => sf.StudentFeeId).ToListAsync();
            return studentFees;
        }

        public async Task<StudentFeeDto> GetStudentFeeByIdAsync(int studentFeeId)
        {
            var studentFee = await _applicationDbContext.StudentFees
                .FirstOrDefaultAsync(sf => sf.StudentFeeId == studentFeeId);

            if (studentFee == null)
            {
                throw new KeyNotFoundException($"Student fee with ID {studentFeeId} not found.");
            }

            return new StudentFeeDto
            {
                StudentFeeId = studentFee.StudentFeeId,
                StudentId = studentFee.StudentId,
                FeeStructureId = studentFee.FeeStructureId,
                Amount = studentFee.Amount,
                DueDate = studentFee.DueDate,
                PaidAmount = studentFee.PaidAmount,
                PaymentStatus = studentFee.PaymentStatus
            };
        }

        public async Task<UpdateStudentFeeDto> UpdateStudentFeeAsync(int studentFeeId, UpdateStudentFeeDto updateStudentFeeDto)
        {
            var studentFee = await _applicationDbContext.StudentFees
                .FirstOrDefaultAsync(sf => sf.StudentFeeId == studentFeeId);
            if (studentFee == null)
            {
                throw new KeyNotFoundException($"Student fee with ID {studentFeeId} not found.");
            }
            studentFee.StudentId = updateStudentFeeDto.StudentId;
            studentFee.FeeStructureId = updateStudentFeeDto.FeeStructureId;
            studentFee.Amount = updateStudentFeeDto.Amount;
            studentFee.DueDate = updateStudentFeeDto.DueDate;
            studentFee.PaidAmount = updateStudentFeeDto.PaidAmount;
            studentFee.PaymentStatus = updateStudentFeeDto.PaymentStatus;
            _applicationDbContext.StudentFees.Update(studentFee);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateStudentFeeDto
            {
                StudentFeeId = studentFee.StudentFeeId,
                StudentId = studentFee.StudentId,
                FeeStructureId = studentFee.FeeStructureId,
                Amount = studentFee.Amount,
                DueDate = studentFee.DueDate,
                PaidAmount = studentFee.PaidAmount,
                PaymentStatus = studentFee.PaymentStatus
            };
        }
    }
}
