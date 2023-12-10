using Core.Entities.Concrete;
using FluentValidation;
namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(c => c.OperationClaimName).NotEmpty().NotNull().MinimumLength(3);
        }
    }
}