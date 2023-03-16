using Domain.Aggregates.AirportAggregate;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Domain.Aggregates.OrderAggregate;
using System;

namespace API.Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            ValidateOrder(request);
            _orderRepository.Update(new Order(request.OrderId, request.CustomerName, request.NumberOfSeats, request.From, request.To, request.BookedDate, request.Status));

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
            
            return new Order();
        }

        private void ValidateOrder(UpdateOrderCommand request)
        {
            var orderDetails = _orderRepository.GetAsync(request.OrderId);
            if (orderDetails.Result.Status.ToLower()== "draft" )
            {
                throw new Exception("The Flight is already approved");
            }
        }
    }
}
