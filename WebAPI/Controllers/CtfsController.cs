using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Ctf;
using Microsoft.AspNetCore.Mvc;

namespace WebApPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CtfsController : Controller
{
    private readonly ICtfService _ctfService;
    private readonly IUserPointService _userPointService;
    private readonly IMapper _mapper;

    public CtfsController(ICtfService ctfService, IUserPointService userPointService, IMapper mapper)
    {
        _ctfService = ctfService;
        _userPointService = userPointService;
        _mapper = mapper;
    }
    
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _ctfService.GetAll();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("getallactive")]
    public IActionResult GetAllActive()
    {
        var result = _ctfService.GetAllActive();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("getalldeactive")]
    public IActionResult GetAllDeActive()
    {
        var result = _ctfService.GetAllDeActive();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
     [HttpPost("add")]
     public IActionResult Add([FromBody] CtfAddDto ctfAddDto)
     {
         var ctf = _mapper.Map<Ctf>(ctfAddDto); 
         var result = _ctfService.Add(ctf);
         if (result.Success)
         {
             return Ok(result);
         }

         return BadRequest(result);
     }
     
     [HttpPut("update")]
     public IActionResult Update([FromBody] CtfUpdateDto ctfUpdateDto)
     {
         var ctf = _mapper.Map<Ctf>(ctfUpdateDto); 
         var result = _ctfService.Update(ctf);
         if (result.Success)
         {
             return Ok(result);
         }

         return BadRequest(result);
     }
     
     [HttpPost("close")]
     public IActionResult Close([FromRoute] Guid ctfId)
     {
         var result = _ctfService.Close(ctfId);
         if (result.Success)
         {
             return Ok(result);
         }

         return BadRequest(result);
     }
     
     [HttpPost("checkanswer")]
     public IActionResult CheckAnswer([FromBody] CheckAnswerDto checkAnswerDto)
     {
        _ctfService.CheckAnswer(checkAnswerDto);
        var result = _userPointService.Solve(new CtfSolve
        {
            Id = Guid.NewGuid(),
            CtfId = checkAnswerDto.CtfId,
        });
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
     }
    
     [HttpPost("addHint")]
     public IActionResult AddHint([FromBody] HintDto hintDto)
     {
         var result = _ctfService.AddHint(hintDto.CtfId, hintDto.Hint);
         if (result.Success)
         {
             return Ok(result);
         }

         return BadRequest(result);
     }
     
     [HttpGet("getHint")]
     public IActionResult GetHint([FromRoute] Guid ctfId)
     {
       
         var result = _ctfService.GetHint(ctfId);
         _userPointService.Add(new UserPoint { Point = -10, Id = Guid.NewGuid() });
         if (result.Success)
         {
             return Ok(result);
         }

         return BadRequest(result);
     }
}