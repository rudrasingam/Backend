using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaff()
        {
            var staff = await _staffService.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDTO>> GetStaff(int id)
        {
            var staffMember = await _staffService.GetStaffByIdAsync(id);
            if (staffMember == null)
            {
                return NotFound();
            }
            return staffMember;
        }

        [HttpPost]
        public async Task<ActionResult<StaffDTO>> CreateStaff([FromBody] StaffDTO staffDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newStaff = await _staffService.CreateStaffAsync(staffDto);
            return CreatedAtAction(nameof(GetStaff), new { id = newStaff.StaffId }, newStaff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffDTO staffDto)
        {
            if (id != staffDto.StaffId)
            {
                return BadRequest();
            }

            await _staffService.UpdateStaffAsync(staffDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            if (!await _staffService.DeleteStaffAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}