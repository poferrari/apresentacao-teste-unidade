using Dapper;
using LojaExemplo.Api.Commands;
using LojaExemplo.Api.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    [Route("v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderRequest createOrderRequest)
        {
            var command = new CreateOrderCommand(createOrderRequest);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Post), result);
        }
    }
}