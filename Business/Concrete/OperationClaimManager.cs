using System;
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
            operationClaim.OperationClaimId = Guid.NewGuid();
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimRules.CheckIfNameExist(operationClaim.OperationClaimName);
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        [CacheAspect]
        public IDataResult<OperationClaim> GetById(Guid operationClaimId)
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
                OperationClaimName = "admin",
                OperationClaimId = Guid.Parse("b1b0a3e3-0b7a-4e1f-8f1a-9c4b1e1b0b1b")
            };
            _operationClaimDal.Add(claim);
        }

        public void AddClaims()
        {
            var claims = new List<string> { "student", "member"};
            foreach (var claim in claims)
            {
                if (_operationClaimDal.Get(c => c.OperationClaimName.Equals(claim)) != null) continue;
                var newClaim = new OperationClaim
                {
                    OperationClaimName = claim,
                    OperationClaimId = Guid.NewGuid()
                };
                _operationClaimDal.Add(newClaim);
            }
        }
    }
}