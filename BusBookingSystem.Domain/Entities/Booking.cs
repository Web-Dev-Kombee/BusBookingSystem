using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingSystem.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string PassengerName { get; set; } 
        public string BusNumber { get; set; } 
        public DateTime TravelDate { get; set; }
        public int SeatNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

