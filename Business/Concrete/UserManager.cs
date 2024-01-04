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
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTO.Get;
using Entities.DTO.Post.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserRules _userRules;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal, IUserRules userRules)
        {
            _userDal = userDal;
            _userRules = userRules;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        
        [CacheAspect]
        public List<OperationClaim> GetClaims(Guid userId)
        {
            return _userDal.GetClaims(userId);
        }
        
        [CacheAspect]
        public IDataResult<UserGetDto> GetByEmailWithDto(string email)
        {
            return new SuccessDataResult<UserGetDto>(_userDal.GetUserDtoByEmail(email));
        }

        [CacheAspect]
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<User> GetById(Guid userId)
        {
         return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == userId));
        }

        [CacheAspect]
        public IDataResult<User> GetByUserName(string userName)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserName.Equals(userName)));
        }
        
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _userRules.CheckIfStudentNumberExist(user.StudentNumber, user.UserId);
            _userRules.CheckIfEmailExist(user.Email, user.UserId);
            _userRules.CheckIfUserNameExist(user.UserName, user.UserId);
            user.UserId = Guid.NewGuid();
            _userDal.Add(user);
            return new SuccessResult();
        }
        
        [SecuredOperation("admin,student,member")]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            var requestedUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.GetAuthenticatedUserId());
            _userRules.CheckIfUpdateUserIdEqualsRequestUserId(user.UserId, requestedUserId);
            _userRules.CheckIfRequestForUpdate(user);
            _userRules.CheckIfStudentNumberExist(user.StudentNumber, user.UserId);
            _userRules.CheckIfUserNameExist(user.UserName, user.UserId);
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
 
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(UserForDelete userForDelete)
        {
            var requestedUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.GetAuthenticatedUserId());
            _userRules.CheckIfUpdateUserIdEqualsRequestUserId(userForDelete.UserId, requestedUserId);
            _userRules.CheckIfUserNotExist(userForDelete.UserId);
            var user = _userDal.Get(u => u.UserId.Equals(userForDelete.UserId));
            user.Status = false;
            _userDal.Update(user);
            return new SuccessResult(Messages.UserDeleted);
        }
        public void AddAdmin()
        {
            if(_userDal.Get(u => u.UserName.Equals("admin")) != null) return;
            HashingHelper.CreatePasswordHash("degistirildibosunaugrasma", out var passwordHash, out var passwordSalt);

            var user = new User
            {
                UserId = Guid.Parse("b1b0a3e3-0b7a-4e1f-8f1a-9c4b1e1b0b1b"),
                Email = "admin@silversoft.com.tr",
                FirstName = "Silversoft",
                LastName = "Admin",
                StudentNumber = "1111111111",
                UserName = "admin",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                Point = 0
            };
            _userDal.Add(user);
        }
    }
}