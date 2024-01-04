using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IUserOperationClaimService.Get, IUserService.GetClaims")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            userOperationClaim.UserOperationClaimId = Guid.NewGuid();
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IUserOperationClaimService.Get, IUserService.GetClaims")]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IUserOperationClaimService.Get, IUserService.GetClaims")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }
        
        public void AddAdminClaimToAdminUser()
        {
            if(_userOperationClaimDal.GetAll().Count != 0) return;
            var adminUserOperationClaim = new UserOperationClaim
            {
                UserId = Guid.Parse("b1b0a3e3-0b7a-4e1f-8f1a-9c4b1e1b0b1b"),
                OperationClaimId = Guid.Parse("b1b0a3e3-0b7a-4e1f-8f1a-9c4b1e1b0b1b")
            };
            _userOperationClaimDal.Add(adminUserOperationClaim);
        }
    }
}