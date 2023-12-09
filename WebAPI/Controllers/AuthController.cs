using Business.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }
}