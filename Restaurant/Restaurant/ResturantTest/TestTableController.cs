using NUnit.Framework;
using Restaurant.Controllers;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.DTO; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace Restaurant.TableControllerTests
{
    [TestFixture]
    public class TablesControllerTests
    {
        private MyDbContext _context;
        private TableController _tablesController;
        private Mock<ILogger<TableController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<TableController>>();
            SeedTestData();
            _tablesController = new TableController(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var tables = new List<Table>
            {
                new Table { TableId = 1, Capacity = 4 },
                new Table { TableId = 2, Capacity = 6 },
                new Table { TableId = 3, Capacity = 2 }
            };
            _context.Tables.AddRange(tables);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetTables_ReturnsAllTables()
        {
            var actionResult = await _tablesController.GetTables();
            var tables = actionResult.Value;
            Assert.That(tables.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetTable_ReturnsTableById()
        {
            int tableId = 1;
            var actionResult = await _tablesController.GetTable(tableId);
            var table = actionResult.Value;
            Assert.That(table.TableId, Is.EqualTo(tableId));
        }

        [Test]
        public async Task GetTable_InvalidId_ReturnsNotFound()
        {
            int invalidTableId = 100;
            var result = await _tablesController.GetTable(invalidTableId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostTable_AddsTable()
        {
            var newTable = new Table { Capacity = 8 };
            var result = await _tablesController.PostTable(newTable);
            var addedTable = await _context.Tables.FindAsync(newTable.TableId);
            Assert.That(addedTable, Is.Not.Null);
            Assert.That(addedTable.Capacity, Is.EqualTo(newTable.Capacity));
        }

        [Test]
        public async Task DeleteTable_RemovesTable()
        {
            int tableId = 1;
            var result = await _tablesController.DeleteTable(tableId);
            var deletedTable = await _context.Tables.FindAsync(tableId);
            Assert.That(deletedTable, Is.Null);
        }
    }
}
