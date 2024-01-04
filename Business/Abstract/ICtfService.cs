using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Ctf;

namespace Business.Abstract;

public interface ICtfService
{
    IResult Add(Ctf ctf);
    IResult Close(Guid ctfId);
    IResult Update(Ctf ctf);
    IResult AddHint(Guid ctfId, string hint);
    IDataResult<List<CtfGetDto>> GetAll();
    IDataResult<List<CtfGetDto>> GetAllActive();
    IDataResult<List<CtfGetDto>> GetAllDeActive();
    IResult CheckAnswer(CheckAnswerDto answerDto);
    IDataResult<HintDto> GetHint(Guid ctfId);
}