using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
        Task<OrderItemDTO> GetOrderItemByIdAsync(int id);
        Task<OrderItemDTO> CreateOrderItemAsync(OrderItemDTO orderItemDto);
        Task UpdateOrderItemAsync(OrderItemDTO orderItemDto);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
