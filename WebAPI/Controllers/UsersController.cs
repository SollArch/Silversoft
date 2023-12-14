using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTO.Get;
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
    
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _userService.GetAll();
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var result = _userService.GetById(id);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getbyusername/{userName}")]
    public IActionResult GetByUserName([FromRoute] string userName)
    {
        var result = _userService.GetByUserName(userName);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getbystudentnumber/{studentNumber}")]
    public IActionResult GetByStudentNumber([FromRoute] string studentNumber)
    {
        var result = _userService.GetByStudentNumber(studentNumber);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getbyemail/{email}")]
    public IActionResult GetByEmail([FromRoute] string email)
    {
        var result = _userService.GetByEmail(email);
        
        if (result.Success)
        {
            var userGetDto = _mapper.Map<UserGetDto>(result.Data);
            return Ok(userGetDto);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getclaims/{userId}")]
    public IActionResult GetClaims([FromRoute] int userId)
    {
        var result = _userService.GetClaims(userId);
            return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] User user)
    {
        var result = _userService.Update(user);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
    
    [HttpPost("delete")]
    public IActionResult Delete([FromBody] User user)
    {
        var result = _userService.Delete(user);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}