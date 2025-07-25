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
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PaymentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreatePaymentDto> CreatePaymentAsync(CreatePaymentDto createPayment)
        {
            var payment = new Payment
            {
                StudentFeeId = createPayment.StudentFeeId,
                Amount = createPayment.Amount,
                PaymentDate = createPayment.PaymentDate,
                PaymentMethod = createPayment.PaymentMethod,
                TransactionReference = createPayment.TransactionReference,
                ReceivedBy = createPayment.ReceivedBy,
                Remarks = createPayment.Remarks
            };
            await _applicationDbContext.Payments.AddAsync(payment);
            await _applicationDbContext.SaveChangesAsync();
            return new CreatePaymentDto
            {
                StudentFeeId = payment.StudentFeeId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                TransactionReference = payment.TransactionReference,
                ReceivedBy = payment.ReceivedBy,
                Remarks = payment.Remarks
            };
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _applicationDbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
            }
            _applicationDbContext.Payments.Remove(payment);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            var payments = await _applicationDbContext.Payments.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    StudentFeeId = p.StudentFeeId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionReference = p.TransactionReference,
                    ReceivedBy = p.ReceivedBy,
                    Remarks = p.Remarks
                }).OrderByDescending(p => p.PaymentId).ToListAsync();
            return payments;
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _applicationDbContext.Payments
                .Where(p => p.PaymentId == id)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    StudentFeeId = p.StudentFeeId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionReference = p.TransactionReference,
                    ReceivedBy = p.ReceivedBy,
                    Remarks = p.Remarks
                }).FirstOrDefaultAsync();
            if (payment == null)
            {
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
            }
            return payment;
        }

        public async Task<UpdatePaymentDto> UpdatePaymentAsync(int id, UpdatePaymentDto updatePayment)
        {
            var payment = await _applicationDbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
            }
            payment.StudentFeeId = updatePayment.StudentFeeId;
            payment.Amount = updatePayment.Amount;
            payment.PaymentDate = updatePayment.PaymentDate;
            payment.PaymentMethod = updatePayment.PaymentMethod;
            payment.TransactionReference = updatePayment.TransactionReference;
            payment.ReceivedBy = updatePayment.ReceivedBy;
            payment.Remarks = updatePayment.Remarks;
            _applicationDbContext.Payments.Update(payment);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdatePaymentDto
            {
                PaymentId = payment.PaymentId,
                StudentFeeId = payment.StudentFeeId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                TransactionReference = payment.TransactionReference,
                ReceivedBy = payment.ReceivedBy,
                Remarks = payment.Remarks
            };
        }
    }
}
