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
using DataAccess.Concrete.EntityFramework;
using Entities.DTO;
using Entities.Enums;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMailService _mailService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public AuthManager(IMailService mailService, ITokenHelper tokenHelper, IUserService userService,
            IUserOperationClaimService userOperationClaimService, IOperationClaimService operationClaimService)
        {
            _mailService = mailService;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _userOperationClaimService = userOperationClaimService;
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
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Point = 0
            };
            SendNewPassword(newUser.Email, newUser.UserName, password);
            _userService.Add(newUser);
            var userOperationClaim = new UserOperationClaim
            {
                UserId = newUser.UserId,
                OperationClaimId = _operationClaimService.GetByName(OperationClaims.NewMember.ToString()).Data
                    .OperationClaimId
            };
            _userOperationClaimService.Add(userOperationClaim);
            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }

        public IResult CheckOtp(CheckOtpDto checkOtpDto)
        {
            throw new System.NotImplementedException();
        }

        public void SendNewPassword(string email, string name, string password)
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
            var userToCheck = _userService.GetByUserName(userForLoginDto.Username).Data;
            var userRules = new UserRules(_userService);
            var result = BusinessRules.Run(
                userRules.CheckIfUserNull(userToCheck),
                userRules.CheckIfUserNotExist(userToCheck.UserId),
                userRules.CheckIfPasswordCorrect(userForLoginDto.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt),
                userRules.CheckIfUserStatus(userToCheck.Status)
            );
            if(result != null) return new ErrorDataResult<User>(result.Message);

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(UserForRegisterDto user)
        {
            var rules = new UserRules(_userService);
            var result = BusinessRules.Run(
                rules.CheckIfEmailExist(user.Email),
                rules.CheckIfUserNameExist(user.UserName),
                rules.CheckIfStudentNumberExist(user.StudentNumber)
            );
            return result ?? new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.UserId);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.PasswordSendToYourMail);
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