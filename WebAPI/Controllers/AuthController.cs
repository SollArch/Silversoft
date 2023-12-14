using Business.Abstract;
using Entities.DTO.Post.Auth;
using Entities.DTO.Post.Otp;
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
        if (!registerResult.Success)
            return BadRequest(registerResult);
        var result = _authService.CreateAccessToken(registerResult.Data);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }
        var result = _authService.CreateAccessToken(userToLogin.Data);
        if (result.Success)
        {
            return Ok(result.Data);
        }
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
            Otp = _otpService.GenerateOtp().Data,
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
            Otp = _otpService.GenerateOtp().Data,
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