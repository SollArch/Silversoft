using Business.Abstract;
using Entities.DTO.Post.Auth;
using Entities.DTO.Post.Otp;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtpsController : Controller
{
    private readonly IOtpService _otpService;
    private readonly IAuthService _authService;
    public OtpsController(IOtpService otpService, IAuthService authService)
    {
        _otpService = otpService;
        _authService = authService;
    }
    
    [HttpPost("checkotpforforgotpassword")]
    public IActionResult CheckOtpForForgotPassword([FromBody] CheckOtpDto checkOtpDto)
    {
        var otpResult = _otpService.CheckOtp(checkOtpDto);
        var user = otpResult.Data;
        if (!otpResult.Success)
        {
            return BadRequest(otpResult.Message);
        }
        var forgotPasswordDto = new ForgotPasswordDto
        {
            Email = user.Email
        };
        
        var result = _authService.ForgotPassword(forgotPasswordDto);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
    
     [HttpPost("checkotpforchangepassword")]
     public IActionResult CheckOtpForChangePassword([FromBody] CheckOtpDto checkOtpDto,[FromQuery] ChangePasswordDto changePasswordDto)
     {
         var result = _authService.ChangePassword(changePasswordDto,checkOtpDto);
         if (result.Success)
         {
             return Ok(result.Message);
         }
         return BadRequest(result.Message);
     }
    
}