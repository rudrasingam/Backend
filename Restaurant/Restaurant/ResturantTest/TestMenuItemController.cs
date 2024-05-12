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

namespace Restaurant.MenuItemControllerTests
{
    [TestFixture]
    public class MenuItemsControllerTests
    {
        private MyDbContext _context;
        private MenuItemsController _menuItemsController;
        private Mock<ILogger<MenuItemsController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<MenuItemsController>>();
            SeedTestData();
            _menuItemsController = new MenuItemsController(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem { MenuItemId = 1, Name = "Burger", Price = 65, TimeToCook = 15, Allergens = "Gluten" },
                new MenuItem { MenuItemId = 2, Name = "Pizza", Price = 70, TimeToCook = 20, Allergens = "Gluten, Dairy" },
                new MenuItem { MenuItemId = 3, Name = "Salad", Price = 30, TimeToCook = 10, Allergens = "None" }
            };
            _context.MenuItems.AddRange(menuItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetMenuItems_ReturnsAllMenuItems()
        {
            var actionResult = await _menuItemsController.GetMenuItems();
            var menuItems = actionResult.Value;
            Assert.That(menuItems.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetMenuItem_ReturnsMenuItemById()
        {
            int menuItemId = 1;
            var actionResult = await _menuItemsController.GetMenuItem(menuItemId);
            var menuItem = actionResult.Value;
            Assert.That(menuItem.MenuItemId, Is.EqualTo(menuItemId));
        }

        [Test]
        public async Task GetMenuItem_InvalidId_ReturnsNotFound()
        {
            int invalidMenuItemId = 100;
            var result = await _menuItemsController.GetMenuItem(invalidMenuItemId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostMenuItem_AddsMenuItem()
        {
            var newMenuItem = new MenuItem { Name = "Pita", Price = 30, TimeToCook = 5, Allergens = "Gluten" };
            var result = await _menuItemsController.PostMenuItem(newMenuItem);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);

            var addedMenuItem = await _context.MenuItems.FindAsync(newMenuItem.MenuItemId);
            Assert.That(addedMenuItem, Is.Not.Null);
            Assert.That(addedMenuItem.Name, Is.EqualTo(newMenuItem.Name));
        }

        [Test]
        public async Task DeleteMenuItem_RemovesMenuItem()
        {
            int menuItemId = 1;
            var result = await _menuItemsController.DeleteMenuItem(menuItemId);
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null);

            var deletedMenuItem = await _context.MenuItems.FindAsync(menuItemId);
            Assert.That(deletedMenuItem, Is.Null);
        }
    }
}
