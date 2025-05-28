using BusBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingSystem.Domain.Interfaces 
{
    public interface IBookingRepository 
    {
        Task AddAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllAsync(int skip, int take);
        Task<int> GetCountAsync();
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Booking>> GetBookingsByBusAndDateAsync(string busNumber, DateTime date);
    }
}
