using FluentValidation;
using GoodHamburger.Application.Models.InputModels;

namespace GoodHamburger.API.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Items)
            .NotNull()
            .NotEmpty().WithMessage("O pedido deve conter pelo menos um item.");
    }
}

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.Items)
            .NotNull()
            .NotEmpty().WithMessage("O pedido deve conter pelo menos um item.");
    }
}
