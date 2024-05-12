using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;

public class DatabaseSeeder
{
    private readonly MyDbContext _context;

    public DatabaseSeeder(MyDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        // Seeding Roles
        if (!_context.Roles.Any())
        {
            var roles = new[]
            {
            new Role { RoleName = "Manager" },
            new Role { RoleName = "Waiter" },
            new Role { RoleName = "KitchenStaff" }
        };
            _context.Roles.AddRange(roles);
            _context.SaveChanges();
        }

        // Seeding MenuItems
        if (!_context.MenuItems.Any())
        {
            var menuItems = new[]
            {
            new MenuItem { Name = "Margherita Pizza", Price = 12.00m, TimeToCook = 15},
            new MenuItem { Name = "Vegan Burger", Price = 10.00m, TimeToCook = 10}
        };
            _context.MenuItems.AddRange(menuItems);
            _context.SaveChanges();
        }

        // Seeding Staff
        if (!_context.Staff.Any())
        {
            var staffMembers = new[]
            {
            new Staff { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", RoleId = _context.Roles.FirstOrDefault(r => r.RoleName == "Manager")?.RoleId ?? 0 },
            new Staff { FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", RoleId = _context.Roles.FirstOrDefault(r => r.RoleName == "KitchenStaff")?.RoleId ?? 0 }
        };
            _context.Staff.AddRange(staffMembers);
            _context.SaveChanges();
        }
    }


    public void Clear()
    {
        // Clear all data from all tables
        foreach (var entityType in _context.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            _context.Database.ExecuteSqlRaw($"DELETE FROM {tableName}; DBCC CHECKIDENT ('{tableName}', RESEED, 0);");
        }

        _context.Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
        _context.Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'DELETE FROM ?'");
        _context.Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
    }
}
