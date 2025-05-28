using BusBookingSystem.Application.Commands;
using BusBookingSystem.Application.Dtos;
using BusBookingSystem.Application.Handlers;
using BusBookingSystem.Application.Queries;

namespace BusBookingSystem.Application.Brokers
{
    public class BookingBroker : IBookingBroker
    {
        private readonly CreateBookingHandler _createHandler;
        private readonly GetBookingHandler _getHandler;
        private readonly UpdateBookingHandler _updateHandler;
        private readonly DeleteBookingHandler _deleteHandler;
        private readonly GetAllBookingsHandler _getAllHandler;
        private readonly GetBookingsCountHandler _getCountHandler;
        private readonly GetSeatAvailabilityHandler _getSeatAvailabilityHandler;

        public BookingBroker(
            CreateBookingHandler createHandler,
            GetBookingHandler getHandler,
            UpdateBookingHandler updateHandler,
            DeleteBookingHandler deleteHandler,
            GetAllBookingsHandler getAllHandler,
            GetBookingsCountHandler getCountHandler,
            GetSeatAvailabilityHandler getSeatAvailabilityHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getCountHandler = getCountHandler;
            _getSeatAvailabilityHandler = getSeatAvailabilityHandler;
        }

        public Task<Guid> ExecuteCommandAsync(CreateBookingCommand command)
        {
            return _createHandler.HandleAsync(command);
        }

        public Task<bool> ExecuteCommandAsync(UpdateBookingCommand command, Guid id)
        {
            return _updateHandler.HandleAsync(command, id);
        }

        public Task<bool> ExecuteCommandAsync(DeleteBookingCommand command)
        {
            return _deleteHandler.HandleAsync(command);
        }

        public Task<BookingDto?> ExecuteQueryAsync(GetBookingQuery query)
        {
            return _getHandler.HandleAsync(query);
        }

        public Task<IEnumerable<BookingDto>> ExecuteQueryAsync(GetAllBookingsQuery query)
        {
            return _getAllHandler.HandleAsync(query);
        }

        public Task<int> ExecuteQueryAsync(GetBookingsCountQuery query)
        {
            return _getCountHandler.HandleAsync(query);
        }

        public Task<SeatAvailabilityDto> ExecuteQueryAsync(GetSeatAvailabilityQuery query)
        {
            return _getSeatAvailabilityHandler.HandleAsync(query);
        }
    }
}