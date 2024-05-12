using System;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary keys configuration
            modelBuilder.Entity<Guest>().HasKey(g => g.GuestId);
            modelBuilder.Entity<MenuItem>().HasKey(mi => mi.MenuItemId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.MenuItemId });
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId); // Updated from Reservation to Booking
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Staff>().HasKey(s => s.StaffId);
            modelBuilder.Entity<Table>().HasKey(t => t.TableId);
            modelBuilder.Entity<TimeSlot>().HasKey(ts => ts.TimeSlotId);

            // Add necessary configurations for each entity
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTime) // Ensure the OrderTime property is configured correctly
                .IsRequired();

            // Configure relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TableId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TimeSlot)
                .WithMany(ts => ts.Bookings)
                .HasForeignKey(b => b.TimeSlotId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.MenuItemId);

            // Seed data
            // Uncomment and modify according to actual data requirements
            // modelBuilder.Entity<Role>().HasData(
            //     new Role { RoleId = 1, RoleName = "Manager" },
            //     new Role { RoleId = 2, RoleName = "Server" },
            //     new Role { RoleId = 3, RoleName = "Chef" }
            // );
        }

        // DbSet properties
        public DbSet<Guest> Guests { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Payment> Payments { get; set; }

    }
}
