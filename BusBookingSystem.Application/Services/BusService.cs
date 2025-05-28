using BusBookingSystem.Application.Brokers;
using BusBookingSystem.Application.Common;
using BusBookingSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BusBookingSystem.Application.Services
{
    public class BusService : IBusService
    {
        private readonly IBusBroker _busBroker;
        private readonly ILogger<BusService> _logger;

        public BusService(IBusBroker busBroker, ILogger<BusService> logger)
        {
            _busBroker = busBroker;
            _logger = logger;
        }

        public async Task<IEnumerable<Bus>> GetActiveBusesAsync()
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBuses);
                return await _busBroker.GetActiveBusesAsync();
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
                return await _busBroker.GetByNumberAsync(busNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBusByNumber, busNumber);
                throw;
            }
        }
    }
} 