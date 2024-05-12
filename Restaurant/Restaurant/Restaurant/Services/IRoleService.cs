using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> GetRoleByIdAsync(int id);
        Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto);
        Task UpdateRoleAsync(RoleDTO roleDto);
        Task<bool> DeleteRoleAsync(int id);
    }
}