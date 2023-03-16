using Domain.SeedWork;
using System;

namespace Domain.Aggregates.OrderAggregate
{
    // this class and the viewmodels will have to be changed according to the table structure you have in DB
    public class Order :  Entity, IAggregateRoot
    {
        public Guid OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public int NumberOfSeats { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime BookedDate { get; private set; }
        public string Status { get; private set; }


        public Order(Guid orderId,string customerName, int numberOfSeats, string from, string to, DateTime bookedDate, string status) : this()
        {
            OrderId = orderId;
            CustomerName = customerName;
            NumberOfSeats = numberOfSeats;
            From = from;
            To = to;
            BookedDate = bookedDate;
            Status = status;
        }

        public Order()
        {
        }
    }
}
