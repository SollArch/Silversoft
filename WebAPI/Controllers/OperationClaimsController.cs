using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : Controller
{
    private readonly IOperationClaimService _operationClaimService;

    public OperationClaimsController(IOperationClaimService operationClaimService)
    {
        _operationClaimService = operationClaimService;
    }
    
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _operationClaimService.GetAll();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var result = _operationClaimService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getbyname/{name}")]
    public IActionResult GetByName([FromRoute]string name)
    {
        var result = _operationClaimService.GetByName(name);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody]OperationClaim operationClaim)
    {
        var result = _operationClaimService.Add(operationClaim);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpPut("update")]
    public IActionResult Update([FromBody]OperationClaim operationClaim)
    {
        var result = _operationClaimService.Update(operationClaim);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete([FromBody]OperationClaim operationClaim)
    {
        var result = _operationClaimService.Delete(operationClaim);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
}