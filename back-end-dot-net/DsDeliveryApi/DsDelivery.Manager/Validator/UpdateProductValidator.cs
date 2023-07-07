using DsDelivery.Core.Shared.Dto.Product;
using FluentValidation;

namespace DsDelivery.Manager.Validator;

public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
        Include(new CreateProductValidator());
    }
}
