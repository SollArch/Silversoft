using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminPasswordController :Controller
{
    private readonly IPasswordService _passwordService;

    public AdminPasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    [HttpPost("add/{password}")]
    public IActionResult Add([FromRoute] string password)
    {
        var result = _passwordService.AddPassword(password);
        if (result.Success)
        {
            return Ok();
        }

        return BadRequest(result);
    }
}