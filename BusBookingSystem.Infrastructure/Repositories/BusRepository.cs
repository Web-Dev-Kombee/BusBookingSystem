using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;
using BusBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusBookingSystem.Infrastructure.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly BookingDbContext _context;

        public BusRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bus>> GetActiveBusesAsync()
        {
            return await _context.Buses
                .Where(b => b.IsActive)
                .OrderBy(b => b.BusNumber)
                .ToListAsync();
        }

        public async Task<Bus?> GetByNumberAsync(string busNumber)
        {
            return await _context.Buses.FirstOrDefaultAsync(b => b.BusNumber == busNumber);
        }
    }
} 