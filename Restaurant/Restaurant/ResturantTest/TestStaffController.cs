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

namespace Restaurant.StaffControllerTests
{
    [TestFixture]
    public class StaffControllerTests
    {
        private MyDbContext _context;
        private StaffController _staffController;
        private Mock<ILogger<StaffController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MyDbContext(options);
            _loggerMock = new Mock<ILogger<StaffController>>();
            SeedTestData();
            _staffController = new StaffController(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedTestData()
        {
            var roles = new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Manager" },
                new Role { RoleId = 2, RoleName = "Server" },
                new Role { RoleId = 3, RoleName = "Chef" }
            };
            _context.Roles.AddRange(roles);

            var staffMembers = new List<Staff>
            {
                new Staff { StaffId = 1, FirstName = "John", LastName = "Doe", Age = 25, Email = "john.doe@example.com", RoleId = 1 },
                new Staff { StaffId = 2, FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice.smith@example.com", RoleId = 2 },
                new Staff { StaffId = 3, FirstName = "Bob", LastName = "Johnson", Age = 35, Email = "bob.johnson@example.com", RoleId = 3 }
            };
            _context.Staff.AddRange(staffMembers);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetStaff_ReturnsAllStaffMembers()
        {
            var result = await _staffController.GetStaff();
            var okResult = result.Result as OkObjectResult;
            var staff = okResult.Value as List<StaffDto>;
            Assert.That(staff.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetStaffMember_ReturnsStaffMemberById()
        {
            int staffMemberId = 1;
            var result = await _staffController.GetStaffMember(staffMemberId);
            var okResult = result.Result as OkObjectResult;
            var staff = okResult.Value as StaffDto;
            Assert.That(staff.StaffId, Is.EqualTo(staffMemberId));
        }

        [Test]
        public async Task GetStaffMember_InvalidId_ReturnsNotFound()
        {
            int invalidStaffMemberId = 100;
            var result = await _staffController.GetStaffMember(invalidStaffMemberId);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostStaffMember_AddsStaffMember()
        {
            var newStaffMember = new StaffDto { FirstName = "Emma", LastName = "Thompson", Age = 30, Email = "emma@thompson.com", RoleId = 2 };
            var result = await _staffController.PostStaffMember(newStaffMember);
            var addedStaffMember = await _context.Staff.FindAsync(newStaffMember.StaffId);
            Assert.That(addedStaffMember, Is.Not.Null);
            Assert.That(addedStaffMember.FirstName, Is.EqualTo(newStaffMember.FirstName));
        }

        [Test]
        public async Task DeleteStaffMember_RemovesStaffMember()
        {
            int staffMemberId = 1;
            var result = await _staffController.DeleteStaffMember(staffMemberId);
            var deletedStaffMember = await _context.Staff.FindAsync(staffMemberId);
            Assert.That(deletedStaffMember, Is.Null);
        }
    }
}
