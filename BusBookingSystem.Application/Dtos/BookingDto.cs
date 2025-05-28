using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingSystem.Application.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; } 
        public string PassengerName { get; set; } = string.Empty; 
        public string BusNumber { get; set; } = string.Empty; 
        public DateTime TravelDate { get; set; } 
        public int SeatNumber { get; set; }
        public DateTime CreatedAt { get; set; } 

        // Parameterless constructor
        public BookingDto()
        {
            PassengerName = string.Empty;
            BusNumber = string.Empty;
            TravelDate = DateTime.Now;
            CreatedAt = DateTime.UtcNow;
        }

        // Parameterized constructor
        public BookingDto(Guid id, string passengerName, string busNumber, DateTime travelDate, int seatNumber, DateTime createdAt)
        {
            Id = id;
            PassengerName = passengerName;
            BusNumber = busNumber;
            TravelDate = travelDate;
            SeatNumber = seatNumber;
            CreatedAt = createdAt;
        }
    }
}
