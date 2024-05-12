
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
    public class OrderItemService : IOrderItemService
    {
        private readonly MyDbContext _context;

        public OrderItemService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                .Select(oi => new OrderItemDTO
                {
                    OrderId = oi.OrderId,
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity
                    // Map other fields as needed
                }).ToListAsync();
        }

        public async Task<OrderItemDTO> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            return orderItem != null ? new OrderItemDTO { OrderId = orderItem.OrderId, MenuItemId = orderItem.MenuItemId, Quantity = orderItem.Quantity } : null;
        }

        public async Task<OrderItemDTO> CreateOrderItemAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = new OrderItem { OrderId = orderItemDto.OrderId, MenuItemId = orderItemDto.MenuItemId, Quantity = orderItemDto.Quantity };
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItemDto;
        }

        public async Task UpdateOrderItemAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemDto.OrderItemId);
            if (orderItem != null)
            {
                orderItem.Quantity = orderItemDto.Quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
