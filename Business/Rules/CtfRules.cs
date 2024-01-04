using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.Ctf;

namespace Business.Rules;

public class CtfRules : ICtfRules
{
    private readonly ICtfDal _ctfDal;
    private readonly IUserPointDal _userPointDal;
    private readonly ICtfSolveDal _ctfSolveDal;
    public CtfRules(ICtfDal ctfDal, IUserPointDal userPointDal, ICtfSolveDal ctfSolveDal)
    {
        _ctfDal = ctfDal;
        _userPointDal = userPointDal;
        _ctfSolveDal = ctfSolveDal;
    }


    public void CheckIfCtfNull(Ctf ctf)
    {
        if (ctf == null)
            throw new BusinessException(Messages.CtfNotFound);
    }

    public Ctf SetNonUpdatableFields(Ctf ctf)
    {
        var ctfFromDatabase = _ctfDal.Get(c => c.Id == ctf.Id);
        ctf.IsActive = ctfFromDatabase.IsActive;
        ctf.NumberOfSolve = ctfFromDatabase.NumberOfSolve;
        return ctf;
    }

    public void CheckTheAnswer(string ctfAnswer, string userAnswer)
    {
        if(ctfAnswer != userAnswer)
            throw new BusinessException(Messages.WrongAnswer);
    }

    public void SetCtfPoint(Ctf ctf)
    {
        ctf.Point -= (ctf.NumberOfSolve) *(ctf.Point/ctf.SolvabilityLimit);
    }

    public void SetCtfActivityBySolvabilityLimit(Ctf ctf)
    {
        if (ctf.NumberOfSolve == ctf.SolvabilityLimit)
            ctf.IsActive = false;
    }

    public void CheckIfHintAddUserIsCtfOwner(Ctf ctf, Guid userId)
    {
        if (ctf.UserId != userId)
            throw new BusinessException(Messages.CtfHintAddUserIsNotCtfOwner);
    }

    public void CheckIfUserPointNotEnough(int userPoint, int hintPoint)
    {
        if (userPoint < hintPoint)
            throw new BusinessException(Messages.UserPointNotEnough);
    }

    public void CheckIfSolverIsCtfOwner(UserPoint userPoint, Guid userId)
    {
        if (userPoint.UserId == userId)
        {
            throw new BusinessException(Messages.CtfSolverIsCtfOwner);
        }
    }

    public void CheckCtfIsActive(Ctf ctf)
    {
        if(!ctf.IsActive)
            throw new BusinessException(Messages.CtfIsNotActive);
    }
    public void CheckIfCtfAlreadySolved(Guid ctfId, Guid userId)
    {
        var result = _ctfSolveDal.Get(x => x.CtfId == ctfId && x.UserId == userId);
        if (result != null)
        {
            throw new BusinessException(Messages.CtfAlreadySolved);
        }
    }
}