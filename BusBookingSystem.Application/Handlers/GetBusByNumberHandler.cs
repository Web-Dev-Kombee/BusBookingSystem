using BusBookingSystem.Application.Common;
using BusBookingSystem.Domain.Entities;
using BusBookingSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusBookingSystem.Application.Handlers
{
    public class GetBusByNumberHandler
    {
        private readonly IBusRepository _busRepository;
        private readonly ILogger<GetBusByNumberHandler> _logger;

        public GetBusByNumberHandler(IBusRepository busRepository, ILogger<GetBusByNumberHandler> logger)
        {
            _busRepository = busRepository;
            _logger = logger;
        }

        public async Task<Bus?> HandleAsync(string busNumber)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Bus.FetchingBusByNumber, busNumber);
                return await _busRepository.GetByNumberAsync(busNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Bus.ErrorFetchingBusByNumber, busNumber);
                throw;
            }
        }
    }
} 