using Business.Abstract;
using Core.Mailing;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMailService _mailService;

        public AuthManager(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void SendNewPassword(string email,string name)
        {
            var mail = new Mail
            {
                ToEmail = email,
                Subject = "Welcome to Silversoft",
                TextBody = $"Dear {name} W4D1ES is your first password, please change this later.",
                ToFullName = name,
            };
            _mailService.Send(mail);
        }
    }
}