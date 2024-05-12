
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
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _context;

        public OrderService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    OrderTime = o.OrderTime
                    // Additional properties can be mapped here
                }).ToListAsync();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order != null ? new OrderDTO { OrderId = order.OrderId, OrderTime = order.OrderTime } : null;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = new Order { OrderTime = orderDto.OrderTime };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            orderDto.OrderId = order.OrderId;
            return orderDto;
        }

        public async Task UpdateOrderAsync(OrderDTO orderDto)
        {
            var order = await _context.Orders.FindAsync(orderDto.OrderId);
            if (order != null)
            {
                order.OrderTime = orderDto.OrderTime;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
