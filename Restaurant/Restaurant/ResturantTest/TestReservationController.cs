using NUnit.Framework;
using Restaurant.Controllers;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.DTO; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace Restaurant.ReservationControllerTests
{
    [TestFixture]
    public class ReservationsControllerTests
    {
        private MyDbContext _context;
        private ReservationsController _reservationsController;
        private Mock<ILogger<ReservationsController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<ReservationsController>>();
            SeedTestData();
            _reservationsController = new ReservationsController(_context, _loggerMock.Object);
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
                new Table { TableId = 2, Capacity = 6 }
            };
            _context.Tables.AddRange(tables);

            var guests = new List<Guest>
            {
                new Guest { GuestId = 1, Name = "John", Email = "john@123.com" },
                new Guest { GuestId = 2, Name = "Alice", Email = "alice@123.com" }
            };
            _context.Guests.AddRange(guests);

            var timeSlots = new List<TimeSlot>
            {
                new TimeSlot { TimeSlotId = 1, StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(2) },
                new TimeSlot { TimeSlotId = 2, StartTime = DateTime.Now.AddHours(3), EndTime = DateTime.Now.AddHours(4) }
            };
            _context.TimeSlots.AddRange(timeSlots);

            var reservations = new List<Reservation>
            {
                new Reservation { ReservationId = 1, GuestId = 1, TableId = 1, TimeSlotId = 1, ReservationTime = DateTime.Now.AddHours(1), Duration = 60, SpecialRequests = "" },
                new Reservation { ReservationId = 2, GuestId = 2, TableId = 2, TimeSlotId = 2, ReservationTime = DateTime.Now.AddHours(2), Duration = 60, SpecialRequests = "" }
            };
            _context.Reservations.AddRange(reservations);

            _context.SaveChanges();
        }

        [Test]
        public async Task GetReservations_ReturnsAllReservations()
        {
            var actionResult = await _reservationsController.GetReservations();
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Value, Is.Not.Null);
            var reservations = actionResult.Value;
            Assert.That(reservations.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetReservation_ReturnsReservationById()
        {
            int reservationId = 1;
            var actionResult = await _reservationsController.GetReservation(reservationId);
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Value, Is.Not.Null);
            var reservation = actionResult.Value;
            Assert.That(reservation.ReservationId, Is.EqualTo(reservationId));
        }

        [Test]
        public async Task GetReservation_InvalidId_ReturnsNotFound()
        {
            int invalidReservationId = 100;
            var result = await _reservationsController.GetReservation(invalidReservationId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostReservation_AddsReservation()
        {
            var newReservation = new Reservation { GuestId = 1, TableId = 2, TimeSlotId = 2, ReservationTime = DateTime.Now.AddHours(3), Duration = 60, SpecialRequests = "Near the entrance" };
            var result = await _reservationsController.PostReservation(newReservation);
            Assert.That(result, Is.Not.Null);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);

            var addedReservation = await _context.Reservations.FindAsync(newReservation.ReservationId);
            Assert.That(addedReservation, Is.Not.Null);
            Assert.That(addedReservation.GuestId, Is.EqualTo(newReservation.GuestId));
            Assert.That(addedReservation.TableId, Is.EqualTo(newReservation.TableId));
            Assert.That(addedReservation.TimeSlotId, Is.EqualTo(newReservation.TimeSlotId));
            Assert.That(addedReservation.ReservationTime, Is.EqualTo(newReservation.ReservationTime));
            Assert.That(addedReservation.SpecialRequests, Is.EqualTo(newReservation.SpecialRequests));
        }

        [Test]
        public async Task DeleteReservation_RemovesReservation()
        {
            int reservationId = 1;
            var result = await _reservationsController.DeleteReservation(reservationId);
            Assert.That(result, Is.Not.Null);
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null);

            var deletedReservation = await _context.Reservations.FindAsync(reservationId);
            Assert.That(deletedReservation, Is.Null);
        }
    }
}
