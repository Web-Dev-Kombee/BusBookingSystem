using BusBookingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace BusBookingSystem.Web.Pages
{
    public class TicketDetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TicketDetailsModel> _logger;

        public TicketDetailsModel(IHttpClientFactory httpClientFactory, ILogger<TicketDetailsModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        public BookingDto? Booking { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync([FromQuery(Name = "q")] string? ticketId)
        {
            if (string.IsNullOrEmpty(ticketId))
            {
                ErrorMessage = "No ticket ID provided";
                return Page();
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/bookings/{ticketId}");
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

                // Check if the travel date is in the future
                IsValid = Booking.TravelDate > DateTime.Now;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ticket details for ID: {TicketId}", ticketId);
                ErrorMessage = "An error occurred while retrieving ticket details.";
                return Page();
            }
        }
    }
} 