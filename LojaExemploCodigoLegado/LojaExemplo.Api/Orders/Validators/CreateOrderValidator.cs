using FluentValidation;
using LojaExemplo.Api.Core.Consts;
using LojaExemplo.Api.Orders.Commands;

namespace LojaExemplo.Api.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.CustomerId)
               .NotEmpty()
               .NotNull()
               .WithMessage(ValidationErrorMessageConst.InvalidCustomer);

            RuleFor(c => c.ZipCode)
               .NotEmpty()
               .MaximumLength(8)
               .WithMessage(ValidationErrorMessageConst.InvalidZipcode);

            RuleForEach(x => x.OrderProducts).SetValidator(new CreateOrderProductValidator());
        }
    }
}
