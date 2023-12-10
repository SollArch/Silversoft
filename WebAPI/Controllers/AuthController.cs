using Business.Abstract;
using Core.Utilities.Security.Otp;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IOtpService _otpService;
    private readonly IUserService _userService;
    public AuthController(IAuthService authService, IOtpService otpService, IUserService userService)
    {
        _authService = authService;
        _otpService = otpService;
        _userService = userService;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserForRegisterDto userForRegisterDto)
    {
        var userExists = _authService.UserExists(userForRegisterDto);
        if (!userExists.Success)
        {
            return BadRequest(userExists.Message);
        }

        var registerResult = _authService.Register(userForRegisterDto);
        var result = _authService.CreateAccessToken(registerResult.Data);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }
        var sendOtpDto = new SendOtpDto
        {
            Email = userToLogin.Data.Email,
            Otp = OtpHelper.GenerateOtp(),
            UserName = userToLogin.Data.UserName
        };
        var result = _otpService.SendOtp(sendOtpDto);
        if(result.Success)
            return Ok(result.Message);
        return BadRequest(result.Message);
    }
    
    [HttpPost("forgotpassword")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var user = _userService.GetByEmail(forgotPasswordDto.Email).Data;
        var sendOtpDto = new SendOtpDto
        {
            Email = forgotPasswordDto.Email,
            Otp = OtpHelper.GenerateOtp(),
            UserName = user.UserName
        };
        var result = _otpService.SendOtp(sendOtpDto);
        return Ok(result.Message);
    }
    
    [HttpPost("changepassword")]
    public IActionResult ChangePassword([FromQuery] string email)
    {
        var user = _userService.GetByEmail(email).Data;
        var sendOtpDto = new SendOtpDto
        {
            Email = email,
            Otp = OtpHelper.GenerateOtp(),
            UserName = user.UserName
        };
        var otpResult = _otpService.SendOtp(sendOtpDto);
        if (!otpResult.Success)
        {
            return BadRequest(otpResult);
        }
        return Ok(otpResult);
    }
}