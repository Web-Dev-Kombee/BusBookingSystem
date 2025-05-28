namespace BusBookingSystem.Web.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        // Parameterless constructor
        public ErrorResponse()
        {
            StatusCode = 500;
            Message = string.Empty;
        }

        // Parameterized constructor
        public ErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}