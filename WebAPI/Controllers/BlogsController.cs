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
}