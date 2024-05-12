using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync();
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task<PaymentDTO> CreatePaymentAsync(PaymentDTO paymentDto);
        Task UpdatePaymentAsync(PaymentDTO paymentDto);
        Task<bool> DeletePaymentAsync(int id);
    }
}