using HardkorowyKodsu.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HardkorowyKodsu.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseQueryService _queryService;

        public DatabaseController(IDatabaseQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            var tables = await _queryService.GetTablesAsync();
            return Ok(tables);
        }

        [HttpGet("views")]
        public async Task<IActionResult> GetViews()
        {
            var views = await _queryService.GetViewsAsync();
            return Ok(views);
        }

        [HttpGet("columns/{tableName}")]
        public async Task<IActionResult> GetColumns(string tableName)
        {
            var columns = await _queryService.GetColumnsAsync(tableName);
            return Ok(columns);
        }
    }
}
