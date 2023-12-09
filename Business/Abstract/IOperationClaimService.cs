using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        List<OperationClaim> GetClaims(int userId);
    }
}