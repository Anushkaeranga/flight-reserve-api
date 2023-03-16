using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Infrastructure.Seeds
{
    public class OrderSeeder : FlightsContextSeeder
    {
        public OrderSeeder(FlightsContext flightsContext) : base(flightsContext)
        {
        }

        public override void Seed()
        {
            if (FlightsContext.Orders.Any())
            {
                Console.WriteLine("Skipping Airports seeder because table is not empty.");
                return;
            }
            var random = new Random();
            var orders = new List<Order>()
            {
   
            };

            FlightsContext.Orders.AddRange(orders);
            FlightsContext.SaveChanges();
        }
    }
}
