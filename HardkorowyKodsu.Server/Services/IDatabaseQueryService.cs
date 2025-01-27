using HardkorowyKodsu.Server.Models;

namespace HardkorowyKodsu.Server.Services
{
    public interface IDatabaseQueryService
    {
        Task<List<TableInfo>> GetTablesAsync();
        Task<List<TableInfo>> GetViewsAsync();
        Task<List<ColumnInfo>> GetColumnsAsync(string tableName);
    }
}
