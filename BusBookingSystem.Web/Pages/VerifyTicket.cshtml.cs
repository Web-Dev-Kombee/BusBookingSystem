using BusBookingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace BusBookingSystem.Web.Pages
{
    public class VerifyTicketModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VerifyTicketModel> _logger;

        public VerifyTicketModel(IHttpClientFactory httpClientFactory, ILogger<VerifyTicketModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        public BookingDto? Booking { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid? id, string? @ref)
        {
            if (!id.HasValue)
            {
                return Page();
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/bookings/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = "Ticket not found or has been cancelled.";
                    return Page();
                }

                Booking = await response.Content.ReadFromJsonAsync<BookingDto>();
                
                if (Booking == null)
                {
                    ErrorMessage = "Unable to retrieve ticket details.";
                    return Page();
                }

                // Verify the reference number if provided
                if (!string.IsNullOrEmpty(@ref))
                {
                    var expectedRef = $"{Booking.BusNumber}-{Booking.SeatNumber}";
                    if (@ref != expectedRef)
                    {
                        IsValid = false;
                        ErrorMessage = "Invalid ticket reference number.";
                        return Page();
                    }
                }

                // Check if the travel date is in the future
                IsValid = Booking.TravelDate > DateTime.Now;
                if (!IsValid)
                {
                    ErrorMessage = "This ticket has expired.";
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying ticket with ID: {TicketId}", id);
                ErrorMessage = "An error occurred while verifying the ticket.";
                return Page();
            }
        }
    }
} 