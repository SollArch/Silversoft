using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.Rules;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        
        [CacheRemoveAspect("IOperationClaimService.Get")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            var rules = new OperationClaimRules(this);
            var result = BusinessRules.Run(rules.CheckIfNameExist(operationClaim.OperationClaimName));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        [CacheRemoveAspect("IOperationClaimService.Get")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            var rules = new OperationClaimRules(this);
            var result = BusinessRules.Run(rules.CheckIfNameExist(operationClaim.OperationClaimName));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        [CacheAspect]
        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(c => c.OperationClaimId == operationClaimId));
        }

        [CacheAspect]
        public IDataResult<OperationClaim> GetByName(string operationClaimName)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(c => c.OperationClaimName == operationClaimName));
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }
    }
}