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
        private readonly IOtpService _otpService;
        public AuthManager(IMailService mailService, ITokenHelper tokenHelper, IUserService userService,
            IUserOperationClaimService userOperationClaimService, IOperationClaimService operationClaimService, IOtpService otpService)
        {
            _mailService = mailService;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
            _otpService = otpService;
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
        

        public void SendNewPassword(string email, string name, string password)
        {
            var mail = new Mail
            {
                ToEmail = email,
                Subject = "Yeni parolan burada!",
                TextBody = $"Sevgili {name}, Silversoft'a hoş geldin. İşte yeni parolan: {password} . Lütfen parolanı daha sonra değiştirmeyi ihmal etme.",
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
                rules.CheckIfEmailExist(user.Email,0),
                rules.CheckIfUserNameExist(user.UserName,0),
                rules.CheckIfStudentNumberExist(user.StudentNumber,0)
            );
            return result ?? new SuccessResult();
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
            if (userToCheck == null)
                return new ErrorResult(Messages.UserNotFound);
            
            var rules = new UserRules(_userService);
            var result = BusinessRules.Run(
                rules.CheckIfPasswordCorrect(changePasswordDto.CurrentPassword, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt),
                rules.CheckIfPasswordsSame(changePasswordDto)
            );
            if (result != null) return new ErrorResult(result.Message);
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
            var rules = new UserRules(_userService);
            var result = BusinessRules.Run(
                rules.CheckIfUserNull(user),
                rules.CheckIfUserNameNotExist(user.UserName)
            );
            if (result != null) return new ErrorResult(result.Message);
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