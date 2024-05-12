using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO> GetBookingByIdAsync(int id);
        Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDto);
        Task UpdateBookingAsync(BookingDTO bookingDto);
        Task<bool> DeleteBookingAsync(int id);
    }
}