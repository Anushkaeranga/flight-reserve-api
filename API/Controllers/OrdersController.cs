using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger, IMediator mediator, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> StoreOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                var order = await _mediator.Send(command);

                return Ok(_mapper.Map<OrderViewModel>(order));
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrdersController StoreOrder Error While processing the request  messages : {ex.Message}");
                _logger.LogError($"OrdersController StoreOrder Error While processing the request  stack trace : {ex.StackTrace}");
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            try
            {
                var order = await _mediator.Send(command);
                return Ok("Order successfully Confirmed!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrdersController UpdateOrder Error While processing the request  messages : {ex.Message}");
                _logger.LogError($"OrdersController UpdateOrder Error While processing the request  stack trace : {ex.StackTrace}");
                if (ex.Message.Contains("The Flight is already approved"))
                {
                    return Ok("The Flight is already approved you cannot change this anymore!");
                }
                else                
                {
                    return BadRequest();
                }     
            }

        }
    }
}
