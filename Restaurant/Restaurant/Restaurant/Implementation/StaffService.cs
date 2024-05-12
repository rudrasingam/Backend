
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
    public class StaffService : IStaffService
    {
        private readonly MyDbContext _context;

        public StaffService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllStaffAsync()
        {
            return await _context.Staff
                .Select(s => new StaffDTO
                {
                    StaffId = s.StaffId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Age = s.Age,
                    RoleId = s.RoleId
                }).ToListAsync();
        }

        public async Task<StaffDTO> GetStaffByIdAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            return staff != null ? new StaffDTO
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                Age = staff.Age,
                RoleId = staff.RoleId
            } : null;
        }

        public async Task<StaffDTO> CreateStaffAsync(StaffDTO staffDto)
        {
            var staff = new Staff
            {
                FirstName = staffDto.FirstName,
                LastName = staffDto.LastName,
                Email = staffDto.Email,
                Age = staffDto.Age,
                RoleId = staffDto.RoleId
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            staffDto.StaffId = staff.StaffId;
            return staffDto;
        }

        public async Task UpdateStaffAsync(StaffDTO staffDto)
        {
            var staff = await _context.Staff.FindAsync(staffDto.StaffId);
            if (staff != null)
            {
                staff.FirstName = staffDto.FirstName;
                staff.LastName = staffDto.LastName;
                staff.Email = staffDto.Email;
                staff.Age = staffDto.Age;
                staff.RoleId = staffDto.RoleId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
