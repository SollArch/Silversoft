using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTO.Get;
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


    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var result = _userService.GetById(id);
        if (!result.Success) return BadRequest(result.Message);
        var userGetDto = _mapper.Map<UserGetDto>(result.Data);
        return Ok(userGetDto);
    }

    [HttpGet("getbyusername/{userName}")]
    public IActionResult GetByUserName([FromRoute] string userName)
    {
        var result = _userService.GetByUserName(userName);
        if (!result.Success) return BadRequest(result.Message);
        var userGetDto = _mapper.Map<UserGetDto>(result.Data);
        return Ok(userGetDto);
    }

    [HttpGet("getbystudentnumber/{studentNumber}")]
    public IActionResult GetByStudentNumber([FromRoute] string studentNumber)
    {
        var result = _userService.GetByStudentNumber(studentNumber);
        if (!result.Success) return BadRequest(result.Message);
        var userGetDto = _mapper.Map<UserGetDto>(result.Data);
        return Ok(userGetDto);
    }

    [HttpGet("getbyemail/{email}")]
    public IActionResult GetByEmail([FromRoute] string email)
    {
        var result = _userService.GetByEmail(email);

        if (!result.Success) return BadRequest(result.Message);
        var userGetDto = _mapper.Map<UserGetDto>(result.Data);
        return Ok(userGetDto);
    }

    [HttpGet("getclaims/{userId}")]
    public IActionResult GetClaims([FromRoute] int userId)
    {
        var result = _userService.GetClaims(userId);
        return Ok(result);
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
}