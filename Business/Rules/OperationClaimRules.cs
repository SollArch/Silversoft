using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Rules
{
    public class OperationClaimRules
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimRules( IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        
        public IResult CheckIfNameExist(string operationClaimName)
        {
            var result = _operationClaimService.GetByName(operationClaimName);
            if (result.Data != null)
            {
                return new ErrorResult(Messages.ThisOperationClaimNameAlreadyExists);
            }

            return new SuccessResult();
        }
    }
}