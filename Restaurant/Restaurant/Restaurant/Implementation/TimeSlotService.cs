
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
    public class TimeSlotService : ITimeSlotService
    {
        private readonly MyDbContext _context;

        public TimeSlotService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlotDTO>> GetAllTimeSlotsAsync()
        {
            return await _context.TimeSlots
                .Select(ts => new TimeSlotDTO
                {
                    TimeSlotId = ts.TimeSlotId,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime
                }).ToListAsync();
        }

        public async Task<TimeSlotDTO> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(id);
            return timeSlot != null ? new TimeSlotDTO
            {
                TimeSlotId = timeSlot.TimeSlotId,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            } : null;
        }

        public async Task<TimeSlotDTO> CreateTimeSlotAsync(TimeSlotDTO timeSlotDto)
        {
            var timeSlot = new TimeSlot
            {
                StartTime = timeSlotDto.StartTime,
                EndTime = timeSlotDto.EndTime
            };

            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();
            timeSlotDto.TimeSlotId = timeSlot.TimeSlotId;
            return timeSlotDto;
        }

        public async Task UpdateTimeSlotAsync(TimeSlotDTO timeSlotDto)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(timeSlotDto.TimeSlotId);
            if (timeSlot != null)
            {
                timeSlot.StartTime = timeSlotDto.StartTime;
                timeSlot.EndTime = timeSlotDto.EndTime;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteTimeSlotAsync(int id)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(id);
            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
