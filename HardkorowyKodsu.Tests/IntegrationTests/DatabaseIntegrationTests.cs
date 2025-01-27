using HardkorowyKodsu.Server.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace HardkorowyKodsu.Tests.IntegrationTests
{
    public class DatabaseIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private const string TestTableName = "DimProduct";
        private const string TestViewName = "vDMPrep";
        private const string InvalidTableName = "NonExistentTable";

        public DatabaseIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTables_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/database/tables");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);

            var tables = await response.Content.ReadFromJsonAsync<List<TableInfo>>();
            Assert.NotNull(tables);
            Assert.NotEmpty(tables);
            Assert.Contains(tables, t => t.TableName == TestTableName);
        }

        [Fact]
        public async Task GetTables_ReturnsListOfTables()
        {
            var tables = await _client.GetFromJsonAsync<List<TableInfo>>("/api/database/tables");
            Assert.NotNull(tables);
            Assert.NotEmpty(tables);

            var table = tables.FirstOrDefault(t => t.TableName == TestTableName);
            Assert.NotNull(table);
            Assert.Equal(TestTableName, table.TableName);
        }

        [Fact]
        public async Task GetViews_ReturnsListOfViews()
        {
            var views = await _client.GetFromJsonAsync<List<TableInfo>>("/api/database/views");
            Assert.NotNull(views);
            Assert.NotEmpty(views);

            var view = views.FirstOrDefault(v => v.TableName == TestViewName);
            Assert.NotNull(view);
            Assert.Equal(TestViewName, view.TableName);
        }

        [Fact]
        public async Task GetColumns_ReturnsColumnsForTable()
        {
            var columns = await _client.GetFromJsonAsync<List<ColumnInfo>>($"/api/database/columns/{TestTableName}");
            Assert.NotNull(columns);
            Assert.NotEmpty(columns);

            var column = columns.FirstOrDefault();
            Assert.NotNull(column);
            Assert.False(string.IsNullOrWhiteSpace(column.ColumnName));
            Assert.False(string.IsNullOrWhiteSpace(column.DataType));
        }

        [Fact]
        public async Task GetColumns_ReturnsEmptyListForInvalidTable()
        {
            var columns = await _client.GetFromJsonAsync<List<ColumnInfo>>($"/api/database/columns/{InvalidTableName}");
            Assert.NotNull(columns);
            Assert.Empty(columns);
        }
    }
}