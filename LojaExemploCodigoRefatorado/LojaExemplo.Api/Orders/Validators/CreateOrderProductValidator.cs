using FluentValidation;
using LojaExemplo.Api.Core.Consts;
using LojaExemplo.Api.Orders.Commands;

namespace LojaExemplo.Api.Orders.Validators
{
    public class CreateOrderProductValidator : AbstractValidator<CreateOrderProductRequest>
    {
        public CreateOrderProductValidator()
        {
            RuleFor(c => c.ProductId)
               .NotEmpty()
               .NotNull()
               .WithMessage(ValidationErrorMessageConst.InvalidProduct);

            RuleFor(c => c.Amount)
               .GreaterThan(0)
               .WithMessage(ValidationErrorMessageConst.InvalidAmount);
        }
    }
}
