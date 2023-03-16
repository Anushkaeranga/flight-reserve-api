using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; private set; }
        public int NumberOfSeats { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime BookedDate { get; private set; }
        public string Status { get; private set; }
        public UpdateOrderCommand(Guid orderId,string customerName, int numberOfSeats, string from, string to, DateTime bookedDate, string status)
        {
            OrderId = orderId;
            CustomerName = customerName;
            NumberOfSeats = numberOfSeats;
            From = from;      
            To = to;
            BookedDate = bookedDate;
            Status = status;
        }   
    }
}
