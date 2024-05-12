using Restaurant.Data;
using Restaurant.DTO;
using Restaurant.Models;
using Restaurant.Services;
using Microsoft.EntityFrameworkCore;


namespace Restaurant.Implementation
{
    public class MenuItemService : IMenuItemService
    {
        private readonly MyDbContext _context;

        public MenuItemService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems
                .Select(mi => new MenuItemDTO
                {
                    MenuItemId = mi.MenuItemId,
                    Name = mi.Name,
                    Price = mi.Price,
                    TimeToCook = mi.TimeToCook
                }).ToListAsync();
        }

        public async Task<MenuItemDTO> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null) return null;

            return new MenuItemDTO
            {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                Price = menuItem.Price,
                TimeToCook = menuItem.TimeToCook
            };
        }

        public async Task<MenuItemDTO> CreateMenuItemAsync(MenuItemDTO menuItemDto)
        {
            var menuItem = new MenuItem
            {
                Name = menuItemDto.Name,
                Price = menuItemDto.Price,
                TimeToCook = menuItemDto.TimeToCook
            };

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            menuItemDto.MenuItemId = menuItem.MenuItemId;
            return menuItemDto;
        }

        public async Task UpdateMenuItemAsync(MenuItemDTO menuItemDto)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemDto.MenuItemId);
            if (menuItem != null)
            {
                menuItem.Name = menuItemDto.Name;
                menuItem.Price = menuItemDto.Price;
                menuItem.TimeToCook = menuItemDto.TimeToCook;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
