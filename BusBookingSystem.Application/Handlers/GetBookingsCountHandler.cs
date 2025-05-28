using BusBookingSystem.Application.Queries;
using BusBookingSystem.Domain.Interfaces;

namespace BusBookingSystem.Application.Handlers
{
    public class GetBookingsCountHandler
    {
        private readonly IBookingRepository _repository;

        public GetBookingsCountHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> HandleAsync(GetBookingsCountQuery query)
        {
            return await _repository.GetCountAsync();
        }
    }
} 