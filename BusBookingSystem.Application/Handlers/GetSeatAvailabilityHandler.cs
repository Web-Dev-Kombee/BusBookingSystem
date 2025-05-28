using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class GetSeatAvailabilityHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBusRepository _busRepository;

        public GetSeatAvailabilityHandler(IBookingRepository bookingRepository, IBusRepository busRepository)
        {
            _bookingRepository = bookingRepository;
            _busRepository = busRepository;
        }

        public async Task<SeatAvailabilityDto> HandleAsync(GetSeatAvailabilityQuery query)
        {
            // Get bus details to know total seats
            var bus = await _busRepository.GetByNumberAsync(query.BusNumber);
            if (bus == null)
                throw new Exception($"Bus {query.BusNumber} not found");

            // Get all bookings for this bus on the specified date
            var bookings = await _bookingRepository.GetBookingsByBusAndDateAsync(query.BusNumber, query.TravelDate.Date);

            var seats = new List<SeatDto>();
            for (int i = 1; i <= bus.Capacity; i++)
            {
                var booking = bookings.FirstOrDefault(b => b.SeatNumber == i);
                seats.Add(new SeatDto
                {
                    SeatNumber = i,
                    IsOccupied = booking != null,
                    PassengerName = booking?.PassengerName
                });
            }

            return new SeatAvailabilityDto
            {
                TotalSeats = bus.Capacity,
                Seats = seats
            };
        }
    }
} 