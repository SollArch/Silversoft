using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTO.Post.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }


   
    [HttpPut("update")]
    public IActionResult Update([FromBody] UserForUpdate userFor)
    {
        var user = _mapper.Map<User>(userFor);
        var result = _userService.Update(user);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromBody] UserForDelete userForDelete)
    {
        var result = _userService.Delete(userForDelete);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbyemail/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var result = _userService.GetByEmailWithDto(email);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}