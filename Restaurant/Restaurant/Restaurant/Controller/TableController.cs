using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDTO>> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        [HttpPost]
        public async Task<ActionResult<TableDTO>> CreateTable([FromBody] TableDTO tableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTable = await _tableService.CreateTableAsync(tableDto);
            return CreatedAtAction(nameof(GetTable), new { id = newTable.TableId }, newTable);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] TableDTO tableDto)
        {
            if (id != tableDto.TableId)
            {
                return BadRequest();
            }

            await _tableService.UpdateTableAsync(tableDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            if (!await _tableService.DeleteTableAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

