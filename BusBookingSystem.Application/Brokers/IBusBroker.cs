using BusBookingSystem.Domain.Entities;

namespace BusBookingSystem.Application.Brokers
{
    public interface IBusBroker
    {
        Task<IEnumerable<Bus>> GetActiveBusesAsync();
        Task<Bus?> GetByNumberAsync(string busNumber);
    }
} 