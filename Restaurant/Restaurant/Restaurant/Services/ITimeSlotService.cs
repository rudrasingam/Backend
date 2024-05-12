using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlotDTO>> GetAllTimeSlotsAsync();
        Task<TimeSlotDTO> GetTimeSlotByIdAsync(int id);
        Task<TimeSlotDTO> CreateTimeSlotAsync(TimeSlotDTO timeSlotDto);
        Task UpdateTimeSlotAsync(TimeSlotDTO timeSlotDto);
        Task<bool> DeleteTimeSlotAsync(int id);
    }
}