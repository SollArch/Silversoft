using System;
using Entities.Concrete;
using Entities.DTO.Post.Ctf;

namespace Business.Rules.Abstract;

public interface ICtfRules
{
    public void CheckIfCtfNull(Ctf ctf);
    public void CheckTheAnswer(string ctfAnswer, string userAnswer);
    public void CheckIfCtfIsActive(Ctf ctf);
    public void CheckIfCtfAlreadySolved(Guid ctfId, Guid userId);
    public Ctf SetNonUpdatableFields(Ctf ctf);
    public void SetCtfPoint(Ctf ctf);
    public void SetCtfActivityBySolvabilityLimit(Ctf ctf);
    public void CheckIfHintAddUserIsCtfOwner(Ctf ctf, Guid userId);
    public void CheckIfUserPointNotEnough(int userPoint, int hintPoint);
    
    public void CheckIfSolverIsCtfOwner(UserPoint userPoint, Guid userId);
    
}