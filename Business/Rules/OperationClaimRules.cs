using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Rules
{
    public class OperationClaimRules : IOperationClaimRules
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimRules(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public void CheckIfNameExist(string operationClaimName)
        {
            var result = _operationClaimDal.Get(oc => oc.OperationClaimName.Equals(operationClaimName));
            if (result != null)
            {
                throw new BusinessException(Messages.ThisOperationClaimNameAlreadyExists);
            }
        }
    }
}