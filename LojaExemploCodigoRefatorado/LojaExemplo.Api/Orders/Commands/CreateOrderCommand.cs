using FluentValidation;
using FluentValidation.Results;
using LojaExemplo.Api.Core.Commands;
using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Orders.Reponses;
using LojaExemplo.Api.Orders.Validators;

namespace LojaExemplo.Api.Commands
{
    public class CreateOrderCommand : CommandRequest<OrderMessage>
    {
        private readonly IValidator<CreateOrderRequest> _validator;
        private ValidationResult _validationResult;

        public CreateOrderCommand(CreateOrderRequest createOrderRequest)
        {
            _validator = new CreateOrderValidator();
            CreateOrderRequest = createOrderRequest;
        }

        public CreateOrderRequest CreateOrderRequest { get; private set; }

        public override ValidationResult ValidationResult
        {
            get
            {
                if (_validationResult is null)
                {
                    _validationResult = _validator.Validate(CreateOrderRequest);
                }
                return _validationResult;
            }
        }
    }
}
