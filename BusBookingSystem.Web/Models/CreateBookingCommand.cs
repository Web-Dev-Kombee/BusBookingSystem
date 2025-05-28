using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Web.Models
{
    public class CreateBookingCommand
    {
        [Required(ErrorMessage = "Passenger name is required.")] public string PassengerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bus number is required.")]
        public string BusNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Travel date is required.")]
        public DateTime TravelDate { get; set; }

        [Range(1, 100, ErrorMessage = "Seat number must be between 1 and 100.")]
        public int SeatNumber { get; set; }

        // Parameterless constructor
        public CreateBookingCommand()
        {
            PassengerName = string.Empty;
            BusNumber = string.Empty;
            TravelDate = DateTime.Now.AddDays(1);
            SeatNumber = 1;
        }

        // Parameterized constructor
        public CreateBookingCommand(string passengerName, string busNumber, DateTime travelDate, int seatNumber)
        {
            PassengerName = passengerName;
            BusNumber = busNumber;
            TravelDate = travelDate;
            SeatNumber = seatNumber;
        }
    }

}