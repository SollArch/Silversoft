using Entities.DTO.Post.Auth;
using FluentValidation;

namespace Business.ValidationRules.FluentValdation;

public class RegisterValidator : AbstractValidator<UserForRegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.LastName).NotEmpty();
        RuleFor(u => u.StudentNumber).NotEmpty().Length(10).WithMessage("Öğrenci numarası 10 haneli olmalıdır.");
        RuleFor(u => u.UserName).NotEmpty();
        
    }   
}