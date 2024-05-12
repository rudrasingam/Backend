using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDTO>> GetAllMenuItemsAsync();
        Task<MenuItemDTO> GetMenuItemByIdAsync(int id);
        Task<MenuItemDTO> CreateMenuItemAsync(MenuItemDTO menuItemDto);
        Task UpdateMenuItemAsync(MenuItemDTO menuItemDto);
        Task<bool> DeleteMenuItemAsync(int id);
    }
}