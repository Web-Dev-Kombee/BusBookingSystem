using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class GetBookingHandler
    {
        private readonly IBookingRepository _repository;

        public GetBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookingDto?> HandleAsync(GetBookingQuery query)
        {
            var booking = await _repository.GetByIdAsync(query.Id);
            if (booking == null)
                return null;

            return new BookingDto
            {
                Id = booking.Id,
                PassengerName = booking.PassengerName,
                BusNumber = booking.BusNumber,
                TravelDate = booking.TravelDate,
                SeatNumber = booking.SeatNumber,
                CreatedAt = booking.CreatedAt
            };
        }
    }

}
