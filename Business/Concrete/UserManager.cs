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
            var result = BusinessRules.Run(
                UserRules.CheckIfStudentNumberExist(user.StudentNumber),
                UserRules.CheckIfEmailExist(user.Email),
                UserRules.CheckIfUserNameExist(user.UserName)
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
            var result = BusinessRules.Run(
                UserRules.CheckIfStudentNumberExist(user.StudentNumber),
                UserRules.CheckIfEmailExist(user.Email),
                UserRules.CheckIfUserNameExist(user.UserName)
            );
            if (result != null)
            {
                return result;
            }
            throw new System.NotImplementedException();
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            var result = BusinessRules.Run(UserRules.CheckIfUserNotExist(user.UserId));
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
    }
}