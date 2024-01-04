using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.LastName).NotEmpty();
        RuleFor(u => u.StudentNumber).NotEmpty().Length(10).WithMessage("Öğrenci numarası 10 haneli olmalıdır.");
        RuleFor(u => u.UserName).NotEmpty();
        
    }
}