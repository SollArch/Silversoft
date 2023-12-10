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
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        public List<OperationClaim> GetClaims(int userId)
        {
            return _userDal.GetClaims(userId);
        }
        
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email.Equals(email)));
        }

        public IDataResult<User> GetByUserName(string userName)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserName.Equals(userName)));
        }

        public IDataResult<User> GetByStudentNumber(string studentNumber)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.StudentNumber.Equals(studentNumber)));
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId.Equals(id)));
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var userRules = new UserRules(this);
            var result = BusinessRules.Run(
                userRules.CheckIfStudentNumberExist(user.StudentNumber,user.UserId),
                userRules.CheckIfEmailExist(user.Email,user.UserId),
                userRules.CheckIfUserNameExist(user.UserName,user.UserId)
            );
            if (result != null)
            {
                return result;
            }

            _userDal.Add(user);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            var userRules = new UserRules(this);
            var result = BusinessRules.Run(
                userRules.CheckIfStudentNumberExist(user.StudentNumber,user.UserId),
                userRules.CheckIfEmailExist(user.Email,user.UserId),
                userRules.CheckIfUserNameExist(user.UserName,user.UserId)
            );
            if (result != null)
            {
                return result;
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            var userRules = new UserRules(this);
            var result = BusinessRules.Run(userRules.CheckIfUserNotExist(user.UserId));
            if (result != null) return result;
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
    }
}