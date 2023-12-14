using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim); 
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<UserOperationClaim> GetById(int userOperationClaimId);
        IDataResult<UserOperationClaim> GetByUserId(int userId);
        void AddAdminClaimToAdminUser();

    }
}