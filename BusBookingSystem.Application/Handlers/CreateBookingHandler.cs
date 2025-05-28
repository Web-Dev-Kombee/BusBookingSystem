using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusBookingSystem.Application.Commands;
using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _repository;

        public CreateBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> HandleAsync(CreateBookingCommand command)
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                PassengerName = command.PassengerName,
                BusNumber = command.BusNumber,
                TravelDate = command.TravelDate,
                SeatNumber = command.SeatNumber,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(booking);
            return booking.Id;
        }
    }

}
