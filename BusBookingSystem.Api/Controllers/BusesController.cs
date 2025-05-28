using BusBookingSystem.Application.Common;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;
        private readonly ILogger<BusesController> _logger;

        public BusesController(IBusService busService, ILogger<BusesController> logger)
        {
            _busService = busService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BusDto>>>> GetActiveBuses()
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBuses);
                var buses = await _busService.GetActiveBusesAsync();
                var busesDto = buses.Select(b => new BusDto
                {
                    BusNumber = b.BusNumber,
                    Route = b.Route,
                    Capacity = b.Capacity,
                    IsActive = b.IsActive
                });

                return Ok(ApiResponse<IEnumerable<BusDto>>.SuccessResponse(busesDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBuses);
                return StatusCode(500, ApiResponse<IEnumerable<BusDto>>.ErrorResponse(MessageConstants.Errors.Bus.FetchError));
            }
        }
    }
} 