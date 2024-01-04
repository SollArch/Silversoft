using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTO.Post.CloudinaryConnection;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CloudinaryConnectionsController : Controller
{
    private readonly ICloudinaryConnectionService _cloudinaryConnectionService;
    private readonly IMapper _mapper;

    public CloudinaryConnectionsController(ICloudinaryConnectionService cloudinaryConnectionService, IMapper mapper)
    {
        _cloudinaryConnectionService = cloudinaryConnectionService;
        _mapper = mapper;
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromForm] AddCloudinaryConnectionDto addCloudinaryConnectionDto)
    {
        
        var result = _cloudinaryConnectionService.AddCloudinarySettings(_mapper.Map<CloudinaryConnection>(addCloudinaryConnectionDto));
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPut("update")]
    public IActionResult Update([FromForm] CloudinaryConnection cloudinaryConnection)
    {
        var result = _cloudinaryConnectionService.UpdateCloudinarySettings(cloudinaryConnection);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete(CloudinaryConnection cloudinaryConnection)
    {
        var result = _cloudinaryConnectionService.DeleteCloudinarySettings(cloudinaryConnection);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}