using System.Collections.Generic;

namespace BusBookingSystem.Application.Dtos
{
    public class SeatAvailabilityDto
    {
        public int TotalSeats { get; set; }
        public List<SeatDto> Seats { get; set; } = new();
    }

    public class SeatDto
    {
        public int SeatNumber { get; set; }
        public bool IsOccupied { get; set; }
        public string? PassengerName { get; set; }
    }
} 