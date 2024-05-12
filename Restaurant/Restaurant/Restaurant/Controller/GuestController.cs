using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;

using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: api/Guest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDTO>>> GetAllGuests()
        {
            var guests = await _guestService.GetAllGuestsAsync();
            if (guests == null)
            {
                return NotFound();
            }
            return Ok(guests);
        }

        // GET: api/Guest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDTO>> GetGuest(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return Ok(guest);
        }

        // POST: api/Guest
        [HttpPost]
        public async Task<ActionResult<GuestDTO>> CreateGuest([FromBody] GuestDTO guestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newGuest = await _guestService.CreateGuestAsync(guestDto);
            return CreatedAtAction(nameof(GetGuest), new { id = newGuest.GuestId }, newGuest);
        }

        // PUT: api/Guest/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] GuestDTO guestDto)
        {
            if (id != guestDto.GuestId)
            {
                return BadRequest("ID mismatch.");
            }

            var existingGuest = await _guestService.GetGuestByIdAsync(id);
            if (existingGuest == null)
            {
                return NotFound();
            }

            await _guestService.UpdateGuestAsync(guestDto);
            return NoContent();  // 204 No Content is a typical response for successful PUT requests.
        }

        // DELETE: api/Guest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var success = await _guestService.DeleteGuestAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
