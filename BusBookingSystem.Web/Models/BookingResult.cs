namespace BusBookingSystem.Web.Models
{
    public class BookingResult
    {
        public Guid Id { get; set; }

        // Parameterless constructor
        public BookingResult()
        {
            Id = Guid.Empty;
        }

        // Parameterized constructor
        public BookingResult(Guid id)
        {
            Id = id;
        }
    }
}