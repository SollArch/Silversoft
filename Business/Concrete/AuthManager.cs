using Business.Abstract;
using Business.Constants;
using Business.Rules;
using Core.Entities.Concrete;
using Core.Mailing;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Password;
using Entities.DTO;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMailService _mailService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;
        
        public AuthManager(IMailService mailService, ITokenHelper tokenHelper, IUserService userService, IOperationClaimService operationClaimService)
        {
            _mailService = mailService;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _operationClaimService = operationClaimService;
        }

        public IDataResult<User> Register(UserForRegisterDto user)
        {
            var password = PasswordHelper.GeneratePassword();
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            var newUser = new User
            {
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                StudentNumber = user.StudentNumber,
                UserName = user.UserName
            };
            SendNewPassword(newUser.Email, newUser.UserName, password);
            _userService.Add(newUser);
            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }

        public void SendNewPassword(string email,string name, string password)
        {
            var mail = new Mail
            {
                ToEmail = email,
                Subject = "Welcome to Silversoft",
                TextBody = $"Dear {name} {password} is your first password, please change this later.",
                ToFullName = name,
            };
            _mailService.Send(mail);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            throw new System.NotImplementedException();
        }

        public IResult UserExists(UserForRegisterDto user)
        {
            var result = BusinessRules.Run(
                UserRules.CheckIfEmailExist(user.Email),
                UserRules.CheckIfUserNameExist(user.UserName),
                UserRules.CheckIfStudentNumberExist(user.StudentNumber)
                );
            return result ?? new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _operationClaimService.GetClaims(user.UserId);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            throw new System.NotImplementedException();
        }

        public IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            throw new System.NotImplementedException();
        }
    }
}