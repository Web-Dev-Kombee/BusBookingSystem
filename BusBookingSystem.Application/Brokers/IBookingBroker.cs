using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;

namespace BusBookingSystem.Application.Brokers
{
    public interface IBookingBroker
    {
        Task<Guid> ExecuteCommandAsync(CreateBookingCommand command);
        Task<bool> ExecuteCommandAsync(UpdateBookingCommand command, Guid id);
        Task<bool> ExecuteCommandAsync(DeleteBookingCommand command);
        Task<BookingDto?> ExecuteQueryAsync(GetBookingQuery query);
        Task<IEnumerable<BookingDto>> ExecuteQueryAsync(GetAllBookingsQuery query);
        Task<int> ExecuteQueryAsync(GetBookingsCountQuery query);
        Task<SeatAvailabilityDto> ExecuteQueryAsync(GetSeatAvailabilityQuery query);
    }
}
