using Business.Abstract;
using Entities.DTO;
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
    
    [HttpPost("checkotpforlogin")]
    public IActionResult CheckOtpForLogin([FromBody] CheckOtpDto checkOtpDto)
    {
        var otpResult = _otpService.CheckOtp(checkOtpDto);
        if (!otpResult.Success)
        {
            return BadRequest(otpResult.Message);
        }
        var result = _authService.CreateAccessToken(otpResult.Data);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(otpResult.Message);
    }
}