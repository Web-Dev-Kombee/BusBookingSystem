using BusBookingSystem.Domain.Entities;

namespace BusBookingSystem.Application.Services
{
    public interface IBusService
    {
        Task<IEnumerable<Bus>> GetActiveBusesAsync();
        Task<Bus?> GetByNumberAsync(string busNumber);
    }
} 