using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;

namespace BusBookingSystem.Application.Services
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(CreateBookingCommand command);

        Task<BookingDto?> GetBookingAsync(GetBookingQuery query);

        Task<IEnumerable<BookingDto>> GetAllBookingsAsync(GetAllBookingsQuery query);

        Task<int> GetBookingsCountAsync();

        Task<bool> UpdateBookingAsync(Guid id, UpdateBookingCommand command);

        Task<bool> DeleteBookingAsync(Guid id);

        Task<SeatAvailabilityDto> GetSeatAvailabilityAsync(GetSeatAvailabilityQuery query);
    }
}
