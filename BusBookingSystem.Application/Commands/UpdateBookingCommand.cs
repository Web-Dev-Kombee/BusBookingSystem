using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Application.Commands
{
    public class UpdateBookingCommand
    {
        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string BusNumber { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }

        [Range(1, 100)]
        public int SeatNumber { get; set; }

        // Parameterless constructor
        public UpdateBookingCommand()
        {
            PassengerName = string.Empty;
            BusNumber = string.Empty;
            TravelDate = DateTime.Now.AddDays(1);
            SeatNumber = 1;
        }

        // Parameterized constructor
        public UpdateBookingCommand(string passengerName, string busNumber, DateTime travelDate, int seatNumber)
        {
            PassengerName = passengerName;
            BusNumber = busNumber;
            TravelDate = travelDate;
            SeatNumber = seatNumber;
        }
    }
} 