using LojaExemplo.Api.Commands;
using LojaExemplo.Api.Orders.Reponses;
using LojaExemplo.Api.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderMessage>
    {
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<OrderMessage> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle started");

            try
            {
                if (request is null || request.CreateOrderRequest is null)
                {
                    throw new Exception($"The command \"{nameof(CreateOrderCommand)}\" could not be null.");
                }

                if (!request.IsValid)
                {
                    var errors = request.ValidationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    throw new Exception($"Errors: {string.Join(", ", errors)}.");
                }

                var result = await _orderService.SendOrder(request.CreateOrderRequest);

                _logger.LogInformation("Handle finished");
                return result;
            }
            catch (Exception ext)
            {
                _logger.LogError(ext.Message);
                throw;
            }
        }
    }
}
