using BusBookingSystem.Application.Commands;
using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class UpdateBookingHandler
    {
        private readonly IBookingRepository _repository;

        public UpdateBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(UpdateBookingCommand command, Guid id)
        {
            var booking = new Booking
            {
                Id = id,
                PassengerName = command.PassengerName,
                BusNumber = command.BusNumber,
                TravelDate = command.TravelDate,
                SeatNumber = command.SeatNumber,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.UpdateAsync(booking);
        }
    }
} 