
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
    public class RoleService : IRoleService
    {
        private readonly MyDbContext _context;

        public RoleService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDTO
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                }).ToListAsync();
        }

        public async Task<RoleDTO> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role != null ? new RoleDTO { RoleId = role.RoleId, RoleName = role.RoleName } : null;
        }

        public async Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto)
        {
            var role = new Role { RoleName = roleDto.RoleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            roleDto.RoleId = role.RoleId;
            return roleDto;
        }

        public async Task UpdateRoleAsync(RoleDTO roleDto)
        {
            var role = await _context.Roles.FindAsync(roleDto.RoleId);
            if (role != null)
            {
                role.RoleName = roleDto.RoleName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
