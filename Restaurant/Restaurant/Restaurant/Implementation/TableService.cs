
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
    public class TableService : ITableService
    {
        private readonly MyDbContext _context;

        public TableService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            return await _context.Tables
                .Select(t => new TableDTO
                {
                    TableId = t.TableId,
                    Capacity = t.Capacity
                }).ToListAsync();
        }

        public async Task<TableDTO> GetTableByIdAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            return table != null ? new TableDTO
            {
                TableId = table.TableId,
                Capacity = table.Capacity
            } : null;
        }

        public async Task<TableDTO> CreateTableAsync(TableDTO tableDto)
        {
            var table = new Table
            {
                Capacity = tableDto.Capacity
            };

            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            tableDto.TableId = table.TableId;
            return tableDto;
        }

        public async Task UpdateTableAsync(TableDTO tableDto)
        {
            var table = await _context.Tables.FindAsync(tableDto.TableId);
            if (table != null)
            {
                table.Capacity = tableDto.Capacity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
