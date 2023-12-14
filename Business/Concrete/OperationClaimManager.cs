using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;   
using Business.Rules.Abstract;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IOperationClaimRules _operationClaimRules;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IOperationClaimRules operationClaimRules)
        {
            _operationClaimDal = operationClaimDal;
            _operationClaimRules = operationClaimRules;
        }
        
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimRules.CheckIfNameExist(operationClaim.OperationClaimName);

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimRules.CheckIfNameExist(operationClaim.OperationClaimName);
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        [CacheAspect]
        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(c =>
                c.OperationClaimId == operationClaimId));
        }

        [CacheAspect]
        public IDataResult<OperationClaim> GetByName(string operationClaimName)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(c =>
                c.OperationClaimName == operationClaimName));
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public void AddAdminClaim()
        {
            if (_operationClaimDal.Get(c => c.OperationClaimName.Equals("admin")) != null) return;
            var claim = new OperationClaim
            {
                OperationClaimName = "admin"
            };
            _operationClaimDal.Add(claim);
        }
    }
}