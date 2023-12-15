using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTO.Post.User;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserRules _userRules;
        public UserManager(IUserDal userDal, IUserRules userRules)
        {
            _userDal = userDal;
            _userRules = userRules;
        }

        [CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        [CacheAspect]
        public List<OperationClaim> GetClaims(int userId)
        {
            return _userDal.GetClaims(userId);
        }
        
        [CacheAspect]
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email.Equals(email)));
        }

        [CacheAspect]
        public IDataResult<User> GetByUserName(string userName)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserName.Equals(userName)));
        }

        [CacheAspect]
        public IDataResult<User> GetByStudentNumber(string studentNumber)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.StudentNumber.Equals(studentNumber)));
        }
        
        [CacheAspect]
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId.Equals(id)));
        }
        
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _userRules.CheckIfStudentNumberExist(user.StudentNumber, user.UserId);
            _userRules.CheckIfEmailExist(user.Email, user.UserId);
            _userRules.CheckIfUserNameExist(user.UserName, user.UserId);
            _userDal.Add(user);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            var userFromDatabase = _userDal.Get(u => u.UserId.Equals(user.UserId));
            if(user.PasswordHash.Length == 0)
            {
                user.PasswordHash = userFromDatabase.PasswordHash;
                user.PasswordSalt = userFromDatabase.PasswordSalt;
                user.Point = userFromDatabase.Point;
                user.Status = userFromDatabase.Status;
            }
            _userRules.CheckIfStudentNumberExist(user.StudentNumber, user.UserId);
            _userRules.CheckIfEmailExist(user.Email, user.UserId);
            _userRules.CheckIfUserNameExist(user.UserName, user.UserId);
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(UserForDelete userForDelete)
        {
            
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