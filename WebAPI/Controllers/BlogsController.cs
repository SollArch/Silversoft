using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.Blog;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;
    
    public BlogsController(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }
    
    [HttpGet("getallactive")]
    public IActionResult GetAllActive()
    {
        var result = _blogService.GetAllActive();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getallnotactive")]
    public IActionResult GetAllNotActive()
    {
        var result = _blogService.GetAllNotActive();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var result = _blogService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    [HttpGet("getbyauthorid")]
    public IActionResult GetByAuthorId([FromRoute] Guid authorId)
    {
        var result = _blogService.GetByAuthorId(authorId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbytitle")]
    public IActionResult GetByTitle([FromRoute] string title)
    {
        var result = _blogService.GetByTitle(title);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody] BlogAddDto blogAddDto)
    {
        var blog = _mapper.Map<Blog>(blogAddDto);
        var result = _blogService.Add(blog);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPut("update")]
    public IActionResult Update([FromBody] BlogUpdateDto blogUpdateDto)
    {
        var blog = _mapper.Map<Blog>(blogUpdateDto);
        var result = _blogService.Update(blog);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete([FromBody] BlogDeleteDto blogDeleteDto)
    {
        var result = _blogService.Delete(blogDeleteDto);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    
}