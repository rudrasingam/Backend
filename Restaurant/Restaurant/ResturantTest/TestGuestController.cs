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

namespace Restaurant.GuestControllerTests
{
    [TestFixture]
    public class GuestsControllerTests
    {
        private MyDbContext _context;
        private GuestsController _guestsController;
        private Mock<ILogger<GuestsController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<GuestsController>>();
            SeedTestData();
            _guestsController = new GuestsController(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var guests = new List<Guest>
            {
                new Guest { GuestId = 1, Name = "John", Email = "test1@123.com" },
                new Guest { GuestId = 2, Name = "Alice", Email = "test2@123.com" },
                new Guest { GuestId = 3, Name = "Bob", Email = "test3@123.com" }
            };
            _context.Guests.AddRange(guests);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetGuests_ReturnsGuestCount()
        {
            var actionResult = await _guestsController.GetGuests();
            var guests = actionResult.Value;
            Assert.That(guests.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetGuest_ReturnsGuestById()
        {
            int guestId = 1;
            var actionResult = await _guestsController.GetGuest(guestId);
            var result = actionResult.Value;
            Assert.That(result.GuestId, Is.EqualTo(guestId));
        }

        [Test]
        public async Task GetGuest_InvalidId_ReturnsNotFound()
        {
            int invalidGuestId = 100;
            var result = await _guestsController.GetGuest(invalidGuestId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostGuest_AddsGuest()
        {
            var newGuest = new Guest { Name = "Emma", Email = "test4@123.com" };
            var result = await _guestsController.PostGuest(newGuest);
            var addedGuest = await _context.Guests.FindAsync(newGuest.GuestId);
            Assert.That(addedGuest, Is.Not.Null);
            Assert.That(addedGuest.Name, Is.EqualTo(newGuest.Name));
        }
    }
}
