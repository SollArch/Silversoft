using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class UserPointManager : IUserPointService
{
    private readonly IUserPointDal _userPointDal;
    private readonly ICtfSolveService _ctfSolveService;

    public UserPointManager(IUserPointDal userPointDal, ICtfSolveService ctfSolveService)
    {
        _userPointDal = userPointDal;
        _ctfSolveService = ctfSolveService;
    }

    [SecuredOperation("admin,student,member")]
    [ValidationAspect(typeof(UserPointValidator))]
    public IResult Solve(UserPoint userPoint, Guid ctfId)
    {
        userPoint.Id = Guid.NewGuid();
        _userPointDal.Add(userPoint);
        var ctfSolve = new CtfSolve
        {
            Id = Guid.NewGuid(),
            CtfId = ctfId,
            UserId = userPoint.UserId
        };
        _ctfSolveService.Add(ctfSolve);
        return new SuccessResult(Messages.CtfSolved);
    }

    public IResult Add(UserPoint userPoint)
    {
        _userPointDal.Add(userPoint);
        return new SuccessResult(Messages.PointAdded);
    }
}