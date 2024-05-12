
using Restaurant.Models;
using Restaurant.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.DTO;

namespace Restaurant.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly MyDbContext _context;

        public BookingService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    TableId = b.TableId,
                    GuestId = b.GuestId,
                    TimeSlotId = b.TimeSlotId,
                    Date = b.Date
                }).ToListAsync();
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            return new BookingDTO
            {
                BookingId = booking.BookingId,
                TableId = booking.TableId,
                GuestId = booking.GuestId,
                TimeSlotId = booking.TimeSlotId,
                Date = booking.Date
            };
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDto)
        {
            var booking = new Booking
            {
                TableId = bookingDto.TableId,
                GuestId = bookingDto.GuestId,
                TimeSlotId = bookingDto.TimeSlotId,
                Date = bookingDto.Date
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            bookingDto.BookingId = booking.BookingId;
            return bookingDto;
        }

        public async Task UpdateBookingAsync(BookingDTO bookingDto)
        {
            var booking = await _context.Bookings.FindAsync(bookingDto.BookingId);
            if (booking != null)
            {
                booking.TableId = bookingDto.TableId;
                booking.GuestId = bookingDto.GuestId;
                booking.TimeSlotId = bookingDto.TimeSlotId;
                booking.Date = bookingDto.Date;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
