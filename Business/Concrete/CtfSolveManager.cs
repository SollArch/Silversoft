using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CtfSolveManager : ICtfSolveService
{
    private readonly ICtfSolveDal _ctfSolveDal;

    public CtfSolveManager(ICtfSolveDal ctfSolveDal)
    {
        _ctfSolveDal = ctfSolveDal;
    }

    
    public IResult Add(CtfSolve ctfSolve)
    {
        _ctfSolveDal.Add(ctfSolve);
        return new SuccessResult(); 
    }

    public IDataResult<List<CtfSolve>> GetAll()
    {
        return new SuccessDataResult<List<CtfSolve>>(_ctfSolveDal.GetAll());
    }

    public IDataResult<List<CtfSolve>> GetByUserId(int userId)
    {
        return new SuccessDataResult<List<CtfSolve>>(_ctfSolveDal.GetAll(cs => cs.UserId.Equals(userId)));
    }
}