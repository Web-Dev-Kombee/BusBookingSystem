using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace BusBookingSystem.Web.Pages.Admin
{
    public class EditBookingModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EditBookingModel> _logger;

        public EditBookingModel(IHttpClientFactory httpClientFactory, ILogger<EditBookingModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        [BindProperty]
        public UpdateBookingCommand Booking { get; set; } = new()
        {
            TravelDate = DateTime.Now // Set a default value to avoid the year 0001 issue
        };

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/bookings/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = "Failed to fetch booking details.";
                    return Page();
                }

                var booking = await response.Content.ReadFromJsonAsync<BookingDto>();
                if (booking == null)
                {
                    return NotFound();
                }

                // Ensure we're working with a valid date
                var travelDate = booking.TravelDate;
                if (travelDate.Year < 2000) // If the date is invalid
                {
                    travelDate = DateTime.Now; // Set to current date/time
                }

                Booking = new UpdateBookingCommand
                {
                    PassengerName = booking.PassengerName,
                    BusNumber = booking.BusNumber,
                    TravelDate = travelDate,
                    SeatNumber = booking.SeatNumber
                };

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching booking {BookingId}", id);
                ErrorMessage = "An error occurred while fetching the booking details.";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Ensure the travel date is valid
                if (Booking.TravelDate.Year < 2000)
                {
                    ModelState.AddModelError("Booking.TravelDate", "Please select a valid travel date.");
                    return Page();
                }

                // Get the original booking to preserve the bus number
                var originalResponse = await _httpClient.GetAsync($"api/bookings/{id}");
                if (!originalResponse.IsSuccessStatusCode)
                {
                    ErrorMessage = "Failed to fetch original booking details.";
                    return Page();
                }

                var originalBooking = await originalResponse.Content.ReadFromJsonAsync<BookingDto>();
                if (originalBooking == null)
                {
                    ErrorMessage = "Original booking not found.";
                    return Page();
                }

                // Preserve the original bus number
                Booking.BusNumber = originalBooking.BusNumber;

                var response = await _httpClient.PutAsJsonAsync($"api/bookings/{id}", Booking);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    ErrorMessage = error?.Message ?? "Failed to update booking.";
                    return Page();
                }

                TempData["SuccessMessage"] = "Booking updated successfully.";
                return RedirectToPage("Dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking {BookingId}", id);
                ErrorMessage = "An error occurred while updating the booking.";
                return Page();
            }
        }
    }

    public class ErrorResponse
    {
        public string? Message { get; set; }
    }
} 