using System;

namespace BusBookingSystem.Application.Queries
{
    public class GetSeatAvailabilityQuery
    {
        public string BusNumber { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
    }
} 