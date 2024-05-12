using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/MenuItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            if (menuItems == null)
            {
                return NotFound();
            }
            return Ok(menuItems);
        }

        // GET: api/MenuItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDTO>> GetMenuItem(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        // POST: api/MenuItem
        [HttpPost]
        public async Task<ActionResult<MenuItemDTO>> CreateMenuItem([FromBody] MenuItemDTO menuItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newMenuItem = await _menuItemService.CreateMenuItemAsync(menuItemDto);
            return CreatedAtAction(nameof(GetMenuItem), new { id = newMenuItem.MenuItemId }, newMenuItem);
        }

        // PUT: api/MenuItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDTO menuItemDto)
        {
            if (id != menuItemDto.MenuItemId)
            {
                return BadRequest();
            }

            var existingMenuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (existingMenuItem == null)
            {
                return NotFound();
            }

            await _menuItemService.UpdateMenuItemAsync(menuItemDto);
            return NoContent();
        }

        // DELETE: api/MenuItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (!await _menuItemService.DeleteMenuItemAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
