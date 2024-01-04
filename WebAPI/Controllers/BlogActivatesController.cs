using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogActivatesController : Controller
{
    private readonly IBlogActivateService _blogActivateService;

    public BlogActivatesController(IBlogActivateService blogActivateService)
    {
        _blogActivateService = blogActivateService;
    }
    
    [HttpPost("activate/{blogId}")]
    public IActionResult Activate([FromRoute] Guid blogId)
    {
        var result = _blogActivateService.Activate(blogId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPost("deactivate/{blogId}")]
    public IActionResult Deactivate([FromRoute] Guid blogId)
    {
        var result = _blogActivateService.Deactivate(blogId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
}