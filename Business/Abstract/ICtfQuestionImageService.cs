using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Post.CtfImage;

namespace Business.Abstract;

public interface ICtfQuestionImageService
{
    IResult Add(CtfQuestionImageAddDto ctfQuestionImageAddDto);
    IResult Delete(CtfQuestionImage ctfImage);
    IDataResult<CtfQuestionImage> GetByCtfQuestionId(Guid id);
    IDataResult<CtfQuestionImage> GetById(Guid id);
    
}