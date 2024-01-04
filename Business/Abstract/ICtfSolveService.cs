using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICtfSolveService
{
    public IResult Add(CtfSolve ctfSolver);
    public IDataResult<List<CtfSolve>> GetAll();
    public IDataResult<List<CtfSolve>> GetByUserId(int userId);
}