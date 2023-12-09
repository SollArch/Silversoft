using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.StudentNumber).NotEmpty();
            RuleFor(u => u.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(u => u.UserName).NotEmpty().MinimumLength(3).NotNull();
            RuleFor(u => u.PasswordHash).NotEmpty();
            RuleFor(u => u.PasswordSalt).NotEmpty(); 
            
        }
    }
}