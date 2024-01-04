using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminPasswordController :Controller
{
    private readonly IAdminPasswordService _adminPasswordService;

    public AdminPasswordController(IAdminPasswordService adminPasswordService)
    {
        _adminPasswordService = adminPasswordService;
    }

    [HttpPost("add/{password}")]
    public IActionResult Add([FromRoute] string password)
    {
        var result = _adminPasswordService.AddPassword(password);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}