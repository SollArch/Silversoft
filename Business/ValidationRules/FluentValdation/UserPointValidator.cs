using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class UserPointValidator : AbstractValidator<UserPoint>
{
    public UserPointValidator()
    {
        RuleFor(cs => cs.UserId).NotEmpty();
        RuleFor(cs => cs.Point).NotEmpty();
    }
}