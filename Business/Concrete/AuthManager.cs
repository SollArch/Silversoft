using System;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Validation;
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

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IMailService _mailService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserService _userService;
    private readonly IUserDal _userDal;
    private readonly IOperationClaimService _operationClaimService;
    private readonly IUserOperationClaimDal _userOperationClaimDal;
    private readonly IOtpService _otpService;
    private readonly IUserRules _userRules;
    private readonly IAdminPasswordService _adminPasswordService;
    private readonly IMapper _mapper;

    public AuthManager(IMailService mailService, ITokenHelper tokenHelper, IUserService userService,
        IOperationClaimService operationClaimService,
        IOtpService otpService, IUserRules userRules, IUserOperationClaimDal userOperationClaimDal, IAdminPasswordService adminPasswordService, IUserDal userDal, IMapper mapper)
    {
        _mailService = mailService;
        _tokenHelper = tokenHelper;
        _userService = userService;
        _operationClaimService = operationClaimService;
        _otpService = otpService;
        _userRules = userRules;
        _userOperationClaimDal = userOperationClaimDal;
        _adminPasswordService = adminPasswordService;
        _userDal = userDal;
        _mapper = mapper;
    }

    [ValidationAspect(typeof(RegisterValidator))]
    public IDataResult<User> Register(UserForRegisterDto user)
    {
        var password = PasswordHelper.GeneratePassword();
        HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
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
            OperationClaimId = string.IsNullOrEmpty(newUser.StudentNumber)
                ? _operationClaimService.GetByName("member").Data.OperationClaimId
                : _operationClaimService.GetByName("student").Data.OperationClaimId
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
        var adminPassword = _adminPasswordService.GetPassword();
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

    public IResult ChangePassword(CheckOtpForChangePasswordDto checkOtpForChangePasswordDto)
    {
        var userToCheck = _userDal.Get(u => u.Email == checkOtpForChangePasswordDto.Email);
        checkOtpForChangePasswordDto.Email = userToCheck.Email;
        _userRules.CheckIfUserNull(userToCheck);
        _userRules.CheckIfPasswordsSame(checkOtpForChangePasswordDto);
        _userRules.CheckIfPasswordCorrect(checkOtpForChangePasswordDto.CurrentPassword, userToCheck.PasswordHash,
            userToCheck.PasswordSalt);
        var checkOtpDto = _mapper.Map<CheckOtpDto>(checkOtpForChangePasswordDto);
        var otpResult = _otpService.CheckOtp(checkOtpDto);
        if (!otpResult.Success)
        {
            return otpResult;
        }

        HashingHelper.CreatePasswordHash(checkOtpForChangePasswordDto.NewPassword, out var passwordHash,
            out var passwordSalt);
        userToCheck.PasswordHash = passwordHash;
        userToCheck.PasswordSalt = passwordSalt;
        _userService.Update(userToCheck);
        return new SuccessResult(Messages.PasswordChanged);
    }

    public IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var user = _userDal.Get(u => u.Email == forgotPasswordDto.Email);
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