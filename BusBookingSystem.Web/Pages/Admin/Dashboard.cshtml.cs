using BusBookingSystem.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusBookingSystem.Web.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DashboardModel> _logger;
        private const int PageSize = 5;

        public DashboardModel(IHttpClientFactory httpClientFactory, ILogger<DashboardModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        public IEnumerable<BookingDto> Bookings { get; set; } = Array.Empty<BookingDto>();
        public int TotalBookings { get; set; }
        public int TodayBookings => Bookings.Count(b => b.TravelDate.Date == DateTime.Today);
        public int ActiveBuses => Bookings.Select(b => b.BusNumber).Distinct().Count();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            try
            {
                CurrentPage = pageNumber ?? 1;
                if (CurrentPage < 1) CurrentPage = 1;

                // First get total count
                var countResponse = await _httpClient.GetAsync("api/bookings/count");
                if (countResponse.IsSuccessStatusCode)
                {
                    TotalBookings = await countResponse.Content.ReadFromJsonAsync<int>();
                    TotalPages = (int)Math.Ceiling(TotalBookings / (double)PageSize);
                }

                // Then get paginated data
                var skip = (CurrentPage - 1) * PageSize;
                var response = await _httpClient.GetAsync($"api/bookings?skip={skip}&take={PageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var bookings = await response.Content.ReadFromJsonAsync<IEnumerable<BookingDto>>();
                    Bookings = bookings ?? Array.Empty<BookingDto>();
                }
                else
                {
                    _logger.LogError("Failed to fetch bookings. Status code: {StatusCode}", response.StatusCode);
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookings");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteBookingAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/bookings/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to delete booking {BookingId}. Status code: {StatusCode}", id, response.StatusCode);
                    TempData["ErrorMessage"] = "Failed to delete the booking.";
                    return RedirectToPage();
                }

                TempData["SuccessMessage"] = "Booking deleted successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking {BookingId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the booking.";
                return RedirectToPage();
            }
        }
    }

    public class PaginatedResponse<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}