using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using QRCoder;
using System.Web;
using BookingDto = BusBookingSystem.Web.Models.BookingDto;

namespace BusBookingSystem.Web.Pages
{
    public class BookingResultModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public BookingResultModel(
            IHttpClientFactory httpClientFactory,
            ILogger<BookingResultModel> logger,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
            _configuration = configuration;
        }

        public BookingDto? Booking { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string? QrCodeImage { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/bookings/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = "Booking not found.";
                    return;
                }

                Booking = await response.Content.ReadFromJsonAsync<BookingDto>();
                if (Booking != null)
                {
                    GenerateQrCode();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booking with ID: {BookingId}", id);
                ErrorMessage = "An error occurred while retrieving booking details.";
            }
        }

        private void GenerateQrCode()
        {
            if (Booking == null) return;

            try
            {
                // Always use localhost for the QR code URL
                var port = Request.Host.Port ?? 7022; // Default port if not specified
                var baseUrl = $"https://localhost:{port}";
                _logger.LogInformation("Using localhost for QR code: {BaseUrl}", baseUrl);
                
                // Create a ticket details URL
                var urlBuilder = new UriBuilder(baseUrl);
                urlBuilder.Path = "/TicketDetails";
                
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["q"] = Booking.Id.ToString();
                urlBuilder.Query = query.ToString();

                string ticketDetailsUrl = urlBuilder.ToString();
                _logger.LogInformation("Generated ticket details URL: {Url}", ticketDetailsUrl);

                using var qrGenerator = new QRCodeGenerator();
                using var qrCodeData = qrGenerator.CreateQrCode(
                    ticketDetailsUrl,
                    QRCodeGenerator.ECCLevel.M,
                    forceUtf8: true,
                    utf8BOM: false,
                    eciMode: QRCodeGenerator.EciMode.Utf8,
                    requestedVersion: 5
                );
                using var qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(20);
                QrCodeImage = $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating QR code for booking {BookingId}", Booking.Id);
                QrCodeImage = null;
            }
        }
    }
}
