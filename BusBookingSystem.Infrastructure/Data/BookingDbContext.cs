using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusBookingSystem.Infrastructure.Data
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        //public DbSet<Passenger> Passengers { get; set; }

        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .HasKey(b => b.BusNumber);

            modelBuilder.Entity<Bus>()
                .Property(b => b.BusNumber)
                .HasMaxLength(10);

            modelBuilder.Entity<Bus>()
                .Property(b => b.Route)
                .HasMaxLength(100)
                .IsRequired();

            // Seed initial bus data
            modelBuilder.Entity<Bus>().HasData(
                new Bus { BusNumber = "BUS001", Route = "City Center - Airport", Capacity = 50, IsActive = true },
                new Bus { BusNumber = "BUS002", Route = "North Station - South Station", Capacity = 45, IsActive = true },
                new Bus { BusNumber = "BUS003", Route = "East Terminal - West Terminal", Capacity = 40, IsActive = true },
                new Bus { BusNumber = "BUS004", Route = "Shopping Mall - University", Capacity = 35, IsActive = true }
            );

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);
        }
    }
}