using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;
using BusBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusBookingSystem.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync(int skip, int take)
        {
            return await _context.Bookings
                .OrderByDescending(b => b.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Bookings.CountAsync();
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(booking.Id);
            if (existingBooking == null)
                return false;

            _context.Entry(existingBooking).CurrentValues.SetValues(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByBusAndDateAsync(string busNumber, DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.BusNumber == busNumber && b.TravelDate.Date == date.Date)
                .ToListAsync();
        }
    }
}
