using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _repository;

        public GetAllBookingsHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookingDto>> HandleAsync(GetAllBookingsQuery query)
        {
            var bookings = await _repository.GetAllAsync(query.Skip, query.Take);
            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                PassengerName = b.PassengerName,
                BusNumber = b.BusNumber,
                TravelDate = b.TravelDate,
                SeatNumber = b.SeatNumber,
                CreatedAt = b.CreatedAt
            });
        }
    }
} 