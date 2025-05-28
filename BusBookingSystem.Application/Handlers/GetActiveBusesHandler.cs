using BusBookingSystem.Application.Common;
using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusBookingSystem.Application.Handlers
{
    public class GetActiveBusesHandler
    {
        private readonly IBusRepository _busRepository;
        private readonly ILogger<GetActiveBusesHandler> _logger;

        public GetActiveBusesHandler(IBusRepository busRepository, ILogger<GetActiveBusesHandler> logger)
        {
            _busRepository = busRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Bus>> HandleAsync()
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBuses);
                return await _busRepository.GetActiveBusesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBuses);
                throw;
            }
        }
    }
} 