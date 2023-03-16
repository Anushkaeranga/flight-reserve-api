using Domain.Aggregates.FlightAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightsContext _context;
        private readonly ILogger<FlightRepository> _logger;
        public FlightRepository(FlightsContext context, ILogger<FlightRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Flight Add(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task<Flight> GetAsync(Guid flightId)
        {
            throw new NotImplementedException();
        }


        public void Update(Flight flight)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Flight>> SearchAsync(Guid destinationId)
        {
            try
            {
                _logger.LogDebug("begin FlightRepository SearchAsync");
                var rates =  _context.FlightRates.Where(a => a.Price.Value > 0).ToList();
                var flights = await _context.Flights.Where(a => a.DestinationAirportId == destinationId && rates.Any()).ToListAsync();
            
                _logger.LogDebug("end FlightRepository SearchAsync");
                return flights;
            }
            catch (Exception)
            {
                _logger.LogError("FlightRepositorySearchAsync threw an exception");
                throw;
            }
        }
    }
}
