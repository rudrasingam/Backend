using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return orderItem;
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDTO>> CreateOrderItem([FromBody] OrderItemDTO orderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newOrderItem = await _orderItemService.CreateOrderItemAsync(orderItemDto);
            return CreatedAtAction(nameof(GetOrderItem), new { id = newOrderItem.OrderItemId }, newOrderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDTO orderItemDto)
        {
            if (id != orderItemDto.OrderItemId)
            {
                return BadRequest();
            }

            var existingOrderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }

            await _orderItemService.UpdateOrderItemAsync(orderItemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            if (!await _orderItemService.DeleteOrderItemAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
