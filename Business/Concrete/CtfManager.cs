using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Ctf;

namespace Business.Concrete;

public class CtfManager : ICtfService
{
    private readonly ICtfDal _ctfDal;
    private readonly ICtfRules _ctfRules;
    
    private readonly IUserService _userService;
    private Guid _authenticatedUserId;

    public CtfManager(ICtfDal ctfDal, ICtfRules ctfRules, IUserService userService)
    {
        _ctfDal = ctfDal;
        _ctfRules = ctfRules;
        _userService = userService;
    }

    [SecuredOperation("admin")]
    [ValidationAspect(typeof(CtfValidator))]
    [CacheRemoveAspect("ICtfService.Get")]
    public IResult Add(Ctf ctf)
    {
        ctf.UserId = JwtHelper.GetAuthenticatedUserId();
        ctf.IsActive = true;
        ctf.NumberOfSolve = 0;
        ctf.Id = Guid.NewGuid();
        _ctfDal.Add(ctf);
        return new SuccessResult(Messages.CtfAdded);
    }

    [SecuredOperation("admin")]
    [CacheRemoveAspect("ICtfService.Get")]
    public IResult Close(Guid ctfId)
    {
        var ctf = _ctfDal.Get(c => c.Id == ctfId);
        _ctfRules.CheckIfCtfNull(ctf);
        ctf.IsActive = false;
        _ctfDal.Update(ctf);
        return new SuccessResult();
    }

    [SecuredOperation("admin")]
    [ValidationAspect(typeof(CtfValidator))]
    [CacheRemoveAspect("ICtfService.Get")]
    public IResult Update(Ctf ctf)
    {
        _ctfRules.SetNonUpdatableFields(ctf);
        _ctfDal.Update(ctf);
        return new SuccessResult(Messages.CtfUpdated);
    }

    [CacheAspect]
    public IDataResult<List<CtfGetDto>> GetAll()
    {
        return new SuccessDataResult<List<CtfGetDto>>(_ctfDal.GetAllWithDto());
    }

    [CacheAspect]
    public IDataResult<List<CtfGetDto>> GetAllActive()
    {
        return new SuccessDataResult<List<CtfGetDto>>(_ctfDal.GetAllWithDto());
    }

    [SecuredOperation("admin")]
    [CacheAspect]
    public IDataResult<List<CtfGetDto>> GetAllDeActive()
    {
        return new SuccessDataResult<List<CtfGetDto>>(_ctfDal.GetAllWithDto(c => !c.IsActive));
    }

    [SecuredOperation("admin,student,member")]
    [CacheRemoveAspect("ICtfService.Get")]
    public IResult CheckAnswer(CheckAnswerDto answerDto)
    {
        _authenticatedUserId = JwtHelper.GetAuthenticatedUserId();
        _ctfRules.CheckIfCtfAlreadySolved(answerDto.CtfId, _authenticatedUserId);
        
        var ctf = _ctfDal.Get(c => c.Id == answerDto.CtfId);
        _ctfRules.CheckIfCtfNull(ctf);
        _ctfRules.CheckCtfIsActive(ctf);
        _ctfRules.CheckTheAnswer(ctf.Answer, answerDto.Answer);
        _ctfRules.SetCtfPoint(ctf);
        ctf.NumberOfSolve++;
        _ctfRules.SetCtfActivityBySolvabilityLimit(ctf); 
        _ctfDal.Update(ctf);
        return new SuccessResult();
    }

    [SecuredOperation("admin,student,member")]
    [CacheAspect]
    public IDataResult<HintDto> GetHint(Guid ctfId)
    {
        _authenticatedUserId = JwtHelper.GetAuthenticatedUserId();
        var user = _userService.GetById(_authenticatedUserId).Data;
        _ctfRules.CheckIfUserPointNotEnough(user.Point, 10);
        var ctf = _ctfDal.Get(ctf => ctf.Id == ctfId);
        return new SuccessDataResult<HintDto>(new HintDto { Hint = ctf.Hint });
    }

    [SecuredOperation("admin")]
    [CacheRemoveAspect("ICtfService.Get")]
    public IResult AddHint(Guid ctfId, string hint)
    {
        _authenticatedUserId = JwtHelper.GetAuthenticatedUserId();
        var ctf = _ctfDal.Get(ctf => ctf.Id == ctfId);
        _ctfRules.CheckIfHintAddUserIsCtfOwner(ctf, _authenticatedUserId);
        _ctfRules.CheckIfCtfNull(ctf);
        _ctfRules.CheckCtfIsActive(ctf);
        ctf.Hint += "," + hint;
        _ctfDal.Update(ctf);
        return new SuccessResult();
    }
}