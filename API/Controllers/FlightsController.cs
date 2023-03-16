using API.ApiResponses;
using Domain.Aggregates.FlightAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{

    private readonly IFlightRepository _flightRepository;
    private readonly ILogger<FlightsController> _logger;
    public FlightsController(IFlightRepository flightRepository, ILogger<FlightsController> logger)
    {
        _flightRepository = flightRepository;
        _logger = logger;
    }
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> GetAvailableFlights(Guid destinationAirPortId)
    {
        try
        {
            _logger.LogDebug("begining of GetAvailableFlights");
            var availableFlights = await _flightRepository.SearchAsync(destinationAirPortId);

            if (!availableFlights.Any())
            {
                _logger.LogDebug("GetAvailableFlights : no results found!");
                return NotFound();
            }
            var flightResponses = availableFlights.Select(flight => new FlightResponse
            (
                DepartureAirportCode: flight?.DestinationAirportId.ToString(),
                ArrivalAirportCode: flight?.OriginAirportId.ToString(),
                Departure: flight.Departure,
                Arrival: flight.Arrival,
                PriceFrom:  flight.Rates.Min(a =>a.Price.Value)
            ));
            _logger.LogDebug("end of GetAvailableFlights");
            return Ok(flightResponses);
        }
        catch (Exception ex)
        {
            _logger.LogError( $"Error While processing the request  messages : {ex.Message}");
            _logger.LogError($"Error While processing the request  stack trace : {ex.StackTrace}");
            return BadRequest();
        }
    }
}
