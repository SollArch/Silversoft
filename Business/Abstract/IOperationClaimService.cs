using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);
        IResult Update(OperationClaim operationClaim);
        IResult Delete(OperationClaim operationClaim);
        IDataResult<OperationClaim> GetById(Guid operationClaimId);
        IDataResult<OperationClaim> GetByName(string operationClaimName);
        IDataResult<List<OperationClaim>> GetAll();
        void AddAdminClaim();
        void AddClaims();
    }
}