using Restaurant.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int id);
        Task<TableDTO> CreateTableAsync(TableDTO tableDto);
        Task UpdateTableAsync(TableDTO tableDto);
        Task<bool> DeleteTableAsync(int id);
    }
}