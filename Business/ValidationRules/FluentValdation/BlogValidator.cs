using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class BlogValidator : AbstractValidator<Blog>
{
    public BlogValidator()
    {
        RuleFor(b => b.Content).MinimumLength(50).WithMessage(Messages.BlogContentMinimumLength);
        RuleFor(b => b.Title).MinimumLength(3).WithMessage(Messages.BlogTitleMinimumLength);
        RuleFor(b => b.Title).MaximumLength(100).WithMessage(Messages.BlogTitleMaximumLength);

    }
}