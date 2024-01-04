using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
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
    public IResult Solve(CtfSolve ctfSolve)
    {
        ctfSolve.UserId = JwtHelper.GetAuthenticatedUserId();
        _ctfSolveService.Add(ctfSolve);  
        return new SuccessResult(Messages.CtfSolved);
    }

    public IResult Add(UserPoint userPoint)
    {
        userPoint.UserId = JwtHelper.GetAuthenticatedUserId();
        _userPointDal.Add(userPoint);
        return new SuccessResult(Messages.PointAdded);
    }
}