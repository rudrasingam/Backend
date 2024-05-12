
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
    public class GuestService : IGuestService
    {
        private readonly MyDbContext _context;

        public GuestService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestDTO>> GetAllGuestsAsync()
        {
            return await _context.Guests
                .Select(g => new GuestDTO
                {
                    GuestId = g.GuestId,
                    Name = g.Name,
                    Email = g.Email
                }).ToListAsync();
        }

        public async Task<GuestDTO> GetGuestByIdAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return null;

            return new GuestDTO
            {
                GuestId = guest.GuestId,
                Name = guest.Name,
                Email = guest.Email
            };
        }

        public async Task<GuestDTO> CreateGuestAsync(GuestDTO guestDto)
        {
            var guest = new Guest
            {
                Name = guestDto.Name,
                Email = guestDto.Email
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            guestDto.GuestId = guest.GuestId;
            return guestDto;
        }

        public async Task UpdateGuestAsync(GuestDTO guestDto)
        {
            var guest = await _context.Guests.FindAsync(guestDto.GuestId);
            if (guest != null)
            {
                guest.Name = guestDto.Name;
                guest.Email = guestDto.Email;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteGuestAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
