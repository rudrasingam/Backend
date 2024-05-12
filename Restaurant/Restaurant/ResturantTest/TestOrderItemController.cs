using NUnit.Framework;
using Restaurant.Controllers;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.DTO; 
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace Restaurant.OrderItemControllerTests
{
    [TestFixture]
    public class OrderItemsControllerTests
    {
        private MyDbContext _context;
        private Mock<ILogger<OrderItemsController>> _logger;
        private OrderItemsController _orderItemsController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _logger = new Mock<ILogger<OrderItemsController>>();
            SeedTestData();
            _orderItemsController = new OrderItemsController(_context, _logger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var menuItem = new MenuItem { MenuItemId = 1, Name = "Burger", Price = 10.99m, TimeToCook = 15, Allergens = "Gluten" };
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();

            var order = new Order { OrderId = 1, TotalAmount = 21.98m };
            _context.Orders.Add(order);
            _context.SaveChanges();

            var orderItem = new OrderItem { OrderItemId = 1, OrderId = order.OrderId, MenuItemId = menuItem.MenuItemId, Quantity = 2 };
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetOrderItem_ReturnsOrderItemById()
        {
            int orderItemId = 1;
            var actionResult = await _orderItemsController.GetOrderItem(orderItemId);
            var orderItem = actionResult.Value;
            Assert.That(orderItem.OrderItemId, Is.EqualTo(orderItemId));
        }

        [Test]
        public async Task GetOrderItem_InvalidId_ReturnsNotFound()
        {
            int invalidOrderItemId = 100;
            var result = await _orderItemsController.GetOrderItem(invalidOrderItemId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteOrderItem_RemovesOrderItem()
        {
            int orderId = 1;
            int menuItemId = 1;
            var result = await _orderItemsController.DeleteOrderItem(orderId, menuItemId);
            var deletedOrderItem = await _context.OrderItems.FindAsync(orderId, menuItemId);
            Assert.That(deletedOrderItem, Is.Null);
        }
    }
}
