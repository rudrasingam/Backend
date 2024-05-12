using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> CreateRole([FromBody] RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newRole = await _roleService.CreateRoleAsync(roleDto);
            return CreatedAtAction(nameof(GetRole), new { id = newRole.RoleId }, newRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDTO roleDto)
        {
            if (id != roleDto.RoleId)
            {
                return BadRequest();
            }

            await _roleService.UpdateRoleAsync(roleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!await _roleService.DeleteRoleAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
