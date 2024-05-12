using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlotDTO>>> GetAllTimeSlots()
        {
            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlotDTO>> GetTimeSlot(int id)
        {
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            return timeSlot;
        }

        [HttpPost]
        public async Task<ActionResult<TimeSlotDTO>> CreateTimeSlot([FromBody] TimeSlotDTO timeSlotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTimeSlot = await _timeSlotService.CreateTimeSlotAsync(timeSlotDto);
            return CreatedAtAction(nameof(GetTimeSlot), new { id = newTimeSlot.TimeSlotId }, newTimeSlot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(int id, [FromBody] TimeSlotDTO timeSlotDto)
        {
            if (id != timeSlotDto.TimeSlotId)
            {
                return BadRequest();
            }

            await _timeSlotService.UpdateTimeSlotAsync(timeSlotDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            if (!await _timeSlotService.DeleteTimeSlotAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}