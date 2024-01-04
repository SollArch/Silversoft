using Business.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.BlogImage;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogImagesController : Controller
{
    private readonly IBlogImageService _blogImageService;

    public BlogImagesController(IBlogImageService blogImageService)
    {
        _blogImageService = blogImageService;
    }
    
    [HttpGet("getbyblogid")]
    public IActionResult GetByBlogId(Guid id)
    {
        var result = _blogImageService.GetByBlogId(id);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbyid")]
    public IActionResult GetById(Guid id)
    {
        var result = _blogImageService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromForm] BlogImageAddDto blogImageAddDto)
    {
        var result = _blogImageService.Add(blogImageAddDto);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete(BlogImage blogImage)
    {
        var result = _blogImageService.Delete(blogImage);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}