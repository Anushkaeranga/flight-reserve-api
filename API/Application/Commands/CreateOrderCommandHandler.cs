using Domain.Aggregates.AirportAggregate;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Domain.Aggregates.OrderAggregate;

namespace API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var airport = _orderRepository.Add(new Order(request.OrderId, request.CustomerName, request.NumberOfSeats, request.From, request.To, request.BookedDate, request.Status));

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();

            return airport;
        }
    }
}
