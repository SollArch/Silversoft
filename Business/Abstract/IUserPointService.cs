using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Post.Ctf;

namespace Business.Abstract;

public interface IUserPointService
{
    IResult Solve(UserPoint userPoint, Guid ctfId);
    IResult Add(UserPoint userPoint);

}