using BusBookingSystem.Application.Commands;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class DeleteBookingHandler
    {
        private readonly IBookingRepository _repository;

        public DeleteBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(DeleteBookingCommand command)
        {
            return await _repository.DeleteAsync(command.Id);
        }
    }
} 