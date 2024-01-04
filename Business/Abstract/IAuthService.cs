using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTO.Post.Auth;
using Entities.DTO.Post.Otp;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto user);
     
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(UserForRegisterDto user);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ChangePassword(CheckOtpForChangePasswordDto checkOtpForChangePasswordDto);
        IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        
    }
}