using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestDTO>> GetAllGuestsAsync();
        Task<GuestDTO> GetGuestByIdAsync(int id);
        Task<GuestDTO> CreateGuestAsync(GuestDTO guestDto);
        Task UpdateGuestAsync(GuestDTO guestDto);
        Task<bool> DeleteGuestAsync(int id);
    }
}