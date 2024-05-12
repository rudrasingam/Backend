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

namespace Restaurant.OrderControllerTests
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private MyDbContext _context;
        private OrdersController _ordersController;
        private Mock<ILogger<OrdersController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<OrdersController>>();
            SeedTestData();
            _ordersController = new OrdersController(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var menuItem1 = new MenuItem { MenuItemId = 1, Name = "Burger", Price = 10.99m, TimeToCook = 15, Allergens = "Gluten" };
            var menuItem2 = new MenuItem { MenuItemId = 2, Name = "Pizza", Price = 12.99m, TimeToCook = 20, Allergens = "Gluten, Dairy" };
            _context.MenuItems.AddRange(menuItem1, menuItem2);
            _context.SaveChanges();

            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuItemId = menuItem1.MenuItemId, Quantity = 2 },
                new OrderItem { OrderItemId = 2, OrderId = 1, MenuItemId = menuItem2.MenuItemId, Quantity = 1 },
                new OrderItem { OrderItemId = 3, OrderId = 2, MenuItemId = menuItem1.MenuItemId, Quantity = 3 }
            };

            var orders = new List<Order>
            {
                new Order { OrderId = 1, GuestId = null, TotalAmount = 35.97m, OrderItems = new List<OrderItem> { orderItems[0], orderItems[1] }},
                new Order { OrderId = 2, GuestId = null, TotalAmount = 26.97m, OrderItems = new List<OrderItem> { orderItems[2] }}
            };

            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetOrders_ReturnsAllOrdersWithOrderItems()
        {
            var actionResult = await _ordersController.GetOrders();
            var orders = actionResult.Value;
            Assert.That(orders.Count(), Is.EqualTo(2));
            Assert.That(orders.All(o => o.OrderItems.Any()), Is.True);
        }

        [Test]
        public async Task GetOrder_ReturnsOrderByIdWithOrderItems()
        {
            int orderId = 1;
            var actionResult = await _ordersController.GetOrder(orderId);
            var order = actionResult.Value;
            Assert.That(order.OrderId, Is.EqualTo(orderId));
            Assert.That(order.OrderItems.Any(), Is.True);
        }

        [Test]
        public async Task GetOrder_InvalidId_ReturnsNotFound()
        {
            int invalidOrderId = 100;
            var result = await _ordersController.GetOrder(invalidOrderId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostOrder_AddsOrderWithOrderItems()
        {
            var newOrder = new Order
            {
                GuestId = null,
                TotalAmount = 21.98m,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { MenuItemId = 1, Quantity = 1 },
                    new OrderItem { MenuItemId = 2, Quantity = 2 }
                }
            };

            var result = await _ordersController.PostOrder(newOrder);
            var addedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            Assert.That(addedOrder, Is.Not.Null);
            Assert.That(addedOrder.OrderItems.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteOrder_RemovesOrder()
        {
            int orderId = 1;
            var result = await _ordersController.DeleteOrder(orderId);
            var deletedOrder = await _context.Orders.FindAsync(orderId);
            Assert.That(deletedOrder, Is.Null);
        }
    }
}
