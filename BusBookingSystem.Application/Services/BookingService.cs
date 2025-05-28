using BusBookingSystem.Application.Brokers;
using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Common;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Queries;
using Microsoft.Extensions.Logging;

namespace BusBookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingBroker _broker;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IBookingBroker broker, ILogger<BookingService> logger)
        {
            _broker = broker;
            _logger = logger;
        }

        public async Task<BookingDto?> GetBookingAsync(GetBookingQuery query)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.FetchingBooking, query.Id);
                return await _broker.ExecuteQueryAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorFetchingBooking, query.Id);
                throw;
            }
        }

        public async Task<Guid> CreateBookingAsync(CreateBookingCommand command)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.CreatingBooking);
                return await _broker.ExecuteCommandAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorCreatingBooking);
                throw;
            }
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync(GetAllBookingsQuery query)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.FetchingBookings, query.Skip, query.Take);
                return await _broker.ExecuteQueryAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorFetchingBookings);
                throw;
            }
        }

        public async Task<int> GetBookingsCountAsync()
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.FetchingBookingsCount);
                return await _broker.ExecuteQueryAsync(new GetBookingsCountQuery());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorFetchingBookingsCount);
                throw;
            }
        }

        public async Task<bool> UpdateBookingAsync(Guid id, UpdateBookingCommand command)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.UpdatingBooking, id);
                return await _broker.ExecuteCommandAsync(command, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorUpdatingBooking, id);
                throw;
            }
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(MessageConstants.Logs.Booking.DeletingBooking, id);
                return await _broker.ExecuteCommandAsync(new DeleteBookingCommand { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstants.Logs.Booking.ErrorDeletingBooking, id);
                throw;
            }
        }

        public async Task<SeatAvailabilityDto> GetSeatAvailabilityAsync(GetSeatAvailabilityQuery query)
        {
            try
            {
                _logger.LogInformation("Fetching seat availability for bus {BusNumber} on {TravelDate}", query.BusNumber, query.TravelDate);
                return await _broker.ExecuteQueryAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching seat availability for bus {BusNumber} on {TravelDate}", query.BusNumber, query.TravelDate);
                throw;
            }
        }
    }
}
