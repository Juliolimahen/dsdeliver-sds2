using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Core.Domain;

namespace DsDelivery.Manager.Validator;



public class CreateProductValidator : AbstractValidator<CreateProductDTO>
{
    public CreateProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(product => product.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(product => product.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(product => product.ImageUri)
            .NotEmpty()
            .Must(BeAValidUri).WithMessage("O campo ImageUri deve ser uma URL válida.");
    }

    private bool BeAValidUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out _);
    }
}
