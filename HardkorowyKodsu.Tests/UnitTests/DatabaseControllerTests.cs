using HardkorowyKodsu.Server.Controllers;
using HardkorowyKodsu.Server.Models;
using HardkorowyKodsu.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HardkorowyKodsu.Tests.UnitTests
{
    public class DatabaseControllerTests
    {
        private readonly Mock<IDatabaseQueryService> _queryServiceMock;
        private readonly DatabaseController _controller;

        public DatabaseControllerTests()
        {
            _queryServiceMock = new Mock<IDatabaseQueryService>();

            _controller = new DatabaseController(_queryServiceMock.Object);

            _queryServiceMock.Setup(s => s.GetTablesAsync())
                .ReturnsAsync(new List<TableInfo>
                {
                    new TableInfo { TableName = "TestTable1", SchemaId = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                    new TableInfo { TableName = "TestTable2", SchemaId = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                });

            _queryServiceMock.Setup(s => s.GetViewsAsync())
                .ReturnsAsync(new List<TableInfo>
                  {
                    new TableInfo { TableName = "TestView1", SchemaId = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                    new TableInfo { TableName = "TestView2", SchemaId = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                });

            _queryServiceMock.Setup(s => s.GetColumnsAsync("TestTable"))
                .ReturnsAsync(new List<ColumnInfo>
                {
                    new ColumnInfo { ColumnName = "Id", DataType = "int", MaxLength = null },
                    new ColumnInfo { ColumnName = "Name", DataType = "string", MaxLength = 255 }
                });

            _queryServiceMock.Setup(s => s.GetColumnsAsync("NonExistentTable"))
                .ReturnsAsync(new List<ColumnInfo>());
        }

        [Fact]
        public async Task GetTables_ReturnsListOfTables()
        {
            // Act
            var result = await _controller.GetTables();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tables = Assert.IsType<List<TableInfo>>(okResult.Value);
            Assert.Equal(2, tables.Count);

            var firstTable = tables.First();
            Assert.Equal("TestTable1", firstTable.TableName);
        }

        [Fact]
        public async Task GetViews_ReturnsListOfViews()
        {
            // Act
            var result = await _controller.GetViews();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var views = Assert.IsType<List<TableInfo>>(okResult.Value);
            Assert.Equal(2, views.Count);

            var firstView = views.First();
            Assert.Equal("TestView1", firstView.TableName);
        }

        [Fact]
        public async Task GetColumns_ReturnsColumnsForTable()
        {
            // Act
            var result = await _controller.GetColumns("TestTable");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var columns = Assert.IsType<List<ColumnInfo>>(okResult.Value);
            Assert.Equal(2, columns.Count);

            var firstColumn = columns.First();
            Assert.Equal("Id", firstColumn.ColumnName);
            Assert.Equal("int", firstColumn.DataType);
        }

        [Fact]
        public async Task GetColumns_ReturnsEmptyListForInvalidTable()
        {
            // Act
            var result = await _controller.GetColumns("NonExistentTable");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var columns = Assert.IsType<List<ColumnInfo>>(okResult.Value);
            Assert.Empty(columns);
        }
    }
}