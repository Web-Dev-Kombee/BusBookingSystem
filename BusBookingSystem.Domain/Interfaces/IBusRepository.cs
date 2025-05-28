using BusBookingSystem.Domain.Entities;

namespace BusBookingSystem.Domain.Interfaces
{
    public interface IBusRepository
    {
        Task<IEnumerable<Bus>> GetActiveBusesAsync();
        Task<Bus?> GetByNumberAsync(string busNumber);
    }
} 