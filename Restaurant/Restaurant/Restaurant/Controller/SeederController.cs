using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeederController : ControllerBase
    {
        private readonly DatabaseSeeder _seeder;

        public SeederController(DatabaseSeeder seeder)
        {
            _seeder = seeder;
        }

        // POST: api/Seeder/seed
        [HttpPost("seed")]
        public ActionResult SeedDatabase()
        {
            try
            {
                _seeder.Seed();
                return Ok("Database seeded successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error seeding database: {ex.Message}");
            }
        }

        // POST: api/Seeder/clear
        [HttpPost("clear")]
        public ActionResult ClearDatabase()
        {
            try
            {
                _seeder.Clear();
                return Ok("Database cleared successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error clearing database: {ex.Message}");
            }
        }
    }
}
