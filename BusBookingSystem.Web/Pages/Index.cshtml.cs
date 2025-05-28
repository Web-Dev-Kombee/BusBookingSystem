using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Common;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using CreateBookingCommand = BusBookingSystem.Web.Models.CreateBookingCommand;

namespace BusBookingSystem.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        [BindProperty]
        public CreateBookingCommand BookingCommand { get; set; } = new()
        {
            TravelDate = DateTime.Now.AddMinutes(10)
        };

        public List<BusDto> AvailableBuses { get; set; } = new();
        public SeatAvailabilityDto SeatAvailability { get; set; } = new() { TotalSeats = 0, Seats = new List<SeatDto>() };
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/buses");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<BusDto>>>();
                    if (apiResponse?.Success == true && apiResponse.Data != null)
                    {
                        AvailableBuses = apiResponse.Data.ToList();
                    }
                    else
                    {
                        ErrorMessage = apiResponse?.Message ?? "Failed to load buses.";
                    }
                }
                else
                {
                    ErrorMessage = $"Failed to load buses. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available buses");
                ErrorMessage = "Unable to load available buses.";
            }
        }

        public async Task<IActionResult> OnGetSeatAvailabilityAsync(string busNumber, DateTime travelDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/bookings/seats?busNumber={busNumber}&travelDate={travelDate:yyyy-MM-ddTHH:mm:ss}");
                if (response.IsSuccessStatusCode)
                {
                    SeatAvailability = await response.Content.ReadFromJsonAsync<SeatAvailabilityDto>();
                    return new JsonResult(SeatAvailability);
                }
                return new JsonResult(new { error = "Failed to fetch seat availability" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching seat availability");
                return new JsonResult(new { error = "An error occurred while fetching seat availability" });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Reload the buses list
                return Page();
            }

            try
            {
                var command = new Application.Commands.CreateBookingCommand
                {
                    PassengerName = BookingCommand.PassengerName,
                    BusNumber = BookingCommand.BusNumber,
                    TravelDate = BookingCommand.TravelDate,
                    SeatNumber = BookingCommand.SeatNumber
                };

                var response = await _httpClient.PostAsJsonAsync("api/bookings", command);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<BookingResult>();
                    return RedirectToPage("BookingResult", new { id = result?.Id });
                }

                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                ErrorMessage = error?.Message ?? "Failed to create booking.";
                await OnGetAsync(); // Reload the buses list
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                ErrorMessage = "An error occurred while creating the booking.";
                await OnGetAsync(); // Reload the buses list
                return Page();
            }
        }
    }
}
