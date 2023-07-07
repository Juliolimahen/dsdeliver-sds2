using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;
using FluentValidation;

namespace DsDelivery.Manager.Validator;


public class CreateUserValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(user => user.Positions)
            .NotEmpty()
            .Must(HaveAtLeastOnePosition);
    }

    private bool HaveAtLeastOnePosition(ICollection<ReferencePosition> positions)
    {
        return positions != null && positions.Count > 0;
    }
}
