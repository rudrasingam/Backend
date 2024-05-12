using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDTO>> GetAllStaffAsync();
        Task<StaffDTO> GetStaffByIdAsync(int id);
        Task<StaffDTO> CreateStaffAsync(StaffDTO staffDto);
        Task UpdateStaffAsync(StaffDTO staffDto);
        Task<bool> DeleteStaffAsync(int id);
    }
}