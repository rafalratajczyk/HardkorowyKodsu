using HardkorowyKodsu.Server.Data;
using HardkorowyKodsu.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HardkorowyKodsu.Server.Services
{
    public class DatabaseQueryService : IDatabaseQueryService
    {
        private const string TABLEQUERY = "SELECT name AS TableName, schema_id AS SchemaId, create_date AS CreateDate, modify_date AS ModifyDate FROM sys.tables ORDER BY name";
        private const string VIEWQUERY = "SELECT name AS TableName, schema_id AS SchemaId, create_date AS CreateDate, modify_date AS ModifyDate FROM sys.views ORDER BY name";
        private const string COLUMNQUERY = "SELECT COLUMN_NAME AS ColumnName, DATA_TYPE AS DataType, CHARACTER_MAXIMUM_LENGTH AS MaxLength FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = {0}";
        private readonly DatabaseContext _context;

        public DatabaseQueryService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<TableInfo>> GetTablesAsync()
        {
            return await _context.Tables
                .FromSqlRaw(TABLEQUERY)
                .ToListAsync();
        }

        public async Task<List<TableInfo>> GetViewsAsync()
        {
            var views = await _context.Tables
                .FromSqlRaw(VIEWQUERY)
                .ToListAsync();
            return views;
        }

        public async Task<List<ColumnInfo>> GetColumnsAsync(string tableName)
        {
            return await _context.Columns
                .FromSqlRaw(COLUMNQUERY, tableName)
                .ToListAsync();
        }
    }
}
