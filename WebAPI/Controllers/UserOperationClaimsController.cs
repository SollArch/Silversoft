using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : Controller
{
    private readonly IUserOperationClaimService _operationClaimService;

    public UserOperationClaimsController(IUserOperationClaimService operationClaimService)
    {
        _operationClaimService = operationClaimService;
    }
    
    [HttpGet("getbyuserid/{userId}")]
    public IActionResult GetByUserId([FromRoute] int userId)
    {
        var result = _operationClaimService.GetByUserId(userId);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("getbyuseroperationclaimid/{userOperationClaimId}")]
    public IActionResult GetByUserOperationClaimId([FromRoute] int userOperationClaimId)
    {
        var result = _operationClaimService.GetById(userOperationClaimId);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody] UserOperationClaim userOperationClaim)
    {
        var result = _operationClaimService.Add(userOperationClaim);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
    
    [HttpPut("update")]
    public IActionResult Update([FromBody] UserOperationClaim userOperationClaim)
    {
        var result = _operationClaimService.Update(userOperationClaim);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete([FromBody] UserOperationClaim userOperationClaim)
    {
        var result = _operationClaimService.Delete(userOperationClaim);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}