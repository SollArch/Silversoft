using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class OperationClaimValidator : AbstractValidator<OperationClaim>
{
    public OperationClaimValidator()
    {
        RuleFor(x => x.OperationClaimName).MinimumLength(3).WithMessage(Messages.OperationClaimNameMinimumLength);
    }
}