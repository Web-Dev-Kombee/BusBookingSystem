using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Queries;
using BusBookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("seats")]
        public async Task<IActionResult> GetSeatAvailability([FromQuery] string busNumber, [FromQuery] DateTime travelDate)
        {
            var query = new GetSeatAvailabilityQuery { BusNumber = busNumber, TravelDate = travelDate };
            var result = await _bookingService.GetSeatAvailabilityAsync(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingId = await _bookingService.CreateBookingAsync(command);
            return CreatedAtAction(nameof(GetBooking), new { id = bookingId }, new { Id = bookingId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var query = new GetBookingQuery { Id = id };
            var booking = await _bookingService.GetBookingAsync(query);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var query = new GetAllBookingsQuery { Skip = skip, Take = take };
            var bookings = await _bookingService.GetAllBookingsAsync(query);
            return Ok(bookings);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetBookingsCount()
        {
            var count = await _bookingService.GetBookingsCountAsync();
            return Ok(count);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] UpdateBookingCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _bookingService.UpdateBookingAsync(id, command);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}