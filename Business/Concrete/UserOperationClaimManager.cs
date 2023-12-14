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


        [CacheAspect]
        public IDataResult<UserOperationClaim> GetById(int userOperationClaimId)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(c =>
                c.UserOperationClaimId == userOperationClaimId));
        }

        [CacheAspect]
        public IDataResult<UserOperationClaim> GetByUserId(int userId)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(c => c.UserId == userId));
        }

        public void AddAdminClaimToAdminUser()
        {
            if(_userOperationClaimDal.Get(c => c.UserId == 1) != null) return;
            var adminUserOperationClaim = new UserOperationClaim
            {
                UserId = 1,
                OperationClaimId = 1
            };
            _userOperationClaimDal.Add(adminUserOperationClaim);
        }
    }
}