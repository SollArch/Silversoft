using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : Controller
{
    private readonly IBlogService _blogService;
    
    public BlogsController(IBlogService blogService)
    {
        _blogService = blogService;
    }
    
    [HttpGet("test")]
    public IActionResult Test()
    {
        _blogService.Test();
        var a = new BlogsController(new BlogManager());
        return Ok();
    }
}