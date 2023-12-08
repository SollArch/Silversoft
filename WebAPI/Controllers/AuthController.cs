using Business.Abstract;
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

    [HttpPost]
    public IActionResult Send([FromQuery] string email,[FromQuery] string name)
    {
        _authService.SendNewPassword(email,name);
        return Ok();
    }
}