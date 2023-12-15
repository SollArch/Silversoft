using Business.Abstract;
using Business.Constants;
using Business.Rules;
using Business.Rules.Abstract;
using Core.Entities.Concrete;
using Core.Mailing;
using Core.Utilities.Mailing;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Password;
using DataAccess.Abstract;
using Entities.DTO.Post.Auth;
using Entities.DTO.Post.Otp;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMailService _mailService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOtpService _otpService;
        private readonly IUserRules _userRules;
        private readonly IPasswordService _passwordService;

        public AuthManager(IMailService mailService, ITokenHelper tokenHelper, IUserService userService,
            IOperationClaimService operationClaimService,
            IOtpService otpService, IUserRules userRules, IUserOperationClaimDal userOperationClaimDal, IPasswordService passwordService)
        {
            _mailService = mailService;
            _tokenHelper = tokenHelper;
            _userService = userService;
          _operationClaimService = operationClaimService;
            _otpService = otpService;
            _userRules = userRules;
            _userOperationClaimDal = userOperationClaimDal;
            _passwordService = passwordService;
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
                OperationClaimId = _operationClaimService.GetByName("new member").Data
                    .OperationClaimId
            };
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }


        private void SendNewPassword(string email, string name, string password)
        {
            var mail = new Mail
            {
                ToEmail = email,
                Subject = "Yeni parolan burada!",
                TextBody =
                    $"Sevgili {name}, Silversoft'a hoş geldin. İşte yeni parolan: {password}. Lütfen parolanı daha sonra değiştirmeyi ihmal etme.",
                ToFullName = name
            };
            var adminPassword = _passwordService.GetPassword();
            _mailService.Send(mail,adminPassword);
        }


        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUserName(userForLoginDto.Username).Data;
            _userRules.CheckIfUserNull(userToCheck);
            _userRules.CheckIfUserStatus(userToCheck.Status);
            _userRules.CheckIfPasswordCorrect(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt);
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(UserForRegisterDto user)
        {
            _userRules.CheckIfEmailExist(user.Email);
            _userRules.CheckIfStudentNumberExist(user.StudentNumber);
            _userRules.CheckIfUserNameExist(user.UserName);
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.UserId);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.PasswordSendToYourMail);
        }

        public IResult ChangePassword(ChangePasswordDto changePasswordDto, CheckOtpDto checkOtpDto)
        {
            var userToCheck = _userService.GetByEmail(changePasswordDto.Email).Data;
            _userRules.CheckIfUserNull(userToCheck);
            _userRules.CheckIfPasswordsSame(changePasswordDto);
            _userRules.CheckIfPasswordCorrect(changePasswordDto.CurrentPassword, userToCheck.PasswordHash,
                userToCheck.PasswordSalt);
  
            var otpResult = _otpService.CheckOtp(checkOtpDto);
            if (!otpResult.Success)
            {
                return otpResult;
            }

            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out var passwordHash,
                out var passwordSalt);
            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;
            _userService.Update(userToCheck);
            return new SuccessResult(Messages.PasswordChanged);
        }

        public IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = _userService.GetByEmail(forgotPasswordDto.Email).Data;
            _userRules.CheckIfUserNull(user);
            var password = PasswordHelper.GeneratePassword();
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var updateResult = _userService.Update(user);
            if (!updateResult.Success)
                return updateResult;
            SendNewPassword(user.Email, user.UserName, password);
            return new SuccessResult(Messages.PasswordSendToYourMail);
        }
    }
}