using Business.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.Like;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikesController : Controller
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost("like")]
    public IActionResult Like([FromBody] LikeDto like)
    {
        var result = _likeService.Like(like);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }


    [HttpPost("unlike")]
    public IActionResult Unlike(Like like)
    {
        var result = _likeService.Unlike(like);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}