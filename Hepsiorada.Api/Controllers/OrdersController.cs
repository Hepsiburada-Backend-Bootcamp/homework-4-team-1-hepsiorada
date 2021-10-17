using Hepsiorada.Application.Commands.Order;
using Hepsiorada.Application.Models;
using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Entities.MongoDB;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersSummaries()
        {
            GetOrderSummariesCommand getOrderSummariesCommand = new GetOrderSummariesCommand();
            List<OrderSummary> order = await _mediator.Send(getOrderSummariesCommand);

            return Ok(order);//TODO
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO createDTO)
        {
            CreateOrderCommand createCommand = createDTO.Adapt<CreateOrderCommand>();

            OrderCreateDTO order = await _mediator.Send(createCommand);

            return Ok(order);//TODO
        }
    }
}
