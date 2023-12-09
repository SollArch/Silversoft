using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTO;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto user);
        void SendNewPassword(string email,string name, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(UserForRegisterDto user);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ChangePassword(ChangePasswordDto changePasswordDto);
        IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        
    }
}