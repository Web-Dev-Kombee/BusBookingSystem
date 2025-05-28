using BusBookingSystem.Application.Common;
using BusBookingSystem.Application.Handlers;
using BusBookingSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BusBookingSystem.Application.Brokers
{
    public class BusBroker : IBusBroker
    {
        private readonly GetActiveBusesHandler _getActiveBusesHandler;
        private readonly GetBusByNumberHandler _getBusByNumberHandler;
        private readonly ILogger<BusBroker> _logger;

        public BusBroker(
            GetActiveBusesHandler getActiveBusesHandler,
            GetBusByNumberHandler getBusByNumberHandler,
            ILogger<BusBroker> logger)
        {
            _getActiveBusesHandler = getActiveBusesHandler;
            _getBusByNumberHandler = getBusByNumberHandler;
            _logger = logger;
        }

        public async Task<IEnumerable<Bus>> GetActiveBusesAsync()
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBuses);
                return await _getActiveBusesHandler.HandleAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBuses);
                throw;
            }
        }

        public async Task<Bus?> GetByNumberAsync(string busNumber)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBusByNumber, busNumber);
                return await _getBusByNumberHandler.HandleAsync(busNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBusByNumber, busNumber);
                throw;
            }
        }
    }
} 