
using Restaurant.Models;
using Restaurant.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.DTO;

namespace Restaurant.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly MyDbContext _context;

        public PaymentService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentDTO
                {
                    PaymentId = p.PaymentId,
                    OrderId = p.OrderId,
                    Amount = p.Amount
                }).ToListAsync();
        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            return payment != null ? new PaymentDTO { PaymentId = payment.PaymentId, OrderId = payment.OrderId, Amount = payment.Amount } : null;
        }

        public async Task<PaymentDTO> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = new Payment { OrderId = paymentDto.OrderId, Amount = paymentDto.Amount };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            paymentDto.PaymentId = payment.PaymentId;
            return paymentDto;
        }

        public async Task UpdatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = await _context.Payments.FindAsync(paymentDto.PaymentId);
            if (payment != null)
            {
                payment.Amount = paymentDto.Amount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
