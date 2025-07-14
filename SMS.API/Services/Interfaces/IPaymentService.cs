using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync(int pageNumber, int pageSize);
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<CreatePaymentDto> CreatePaymentAsync(CreatePaymentDto createPayment);
        Task<UpdatePaymentDto> UpdatePaymentAsync(int id, UpdatePaymentDto updatePayment);
        Task<bool> DeletePaymentAsync(int id);
    }
}
