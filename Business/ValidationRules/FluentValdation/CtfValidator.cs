using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class CtfValidator : AbstractValidator<Ctf>
{
    public CtfValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Question).NotEmpty();
        RuleFor(c => c.Answer).NotEmpty();
        RuleFor(c => c.Point).NotEmpty();
        RuleFor(c => c.SolvabilityLimit).NotEmpty();
    }
}