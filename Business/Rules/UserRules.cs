using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;

namespace Business.Rules
{
    public class UserRules
    {
        private readonly IUserService _userService;

        public UserRules( IUserService userService)
        {
            _userService = userService;
        }

        public  IResult CheckIfStudentNumberExist(string studentNumber)
        {
            
            var result = _userService.GetByStudentNumber(studentNumber);
            if (result.Data != null)
            {
                return new ErrorResult(Messages.ThisStudentNumberAlreadyExists);
            }

            return new SuccessResult();
        }

        public IResult CheckIfEmailExist(string userEmail)
        {
            var result = _userService.GetByEmail(userEmail);
            if (result.Data != null)
            {
                return new ErrorResult(Messages.ThisEmailAlreadyExists);
            }

            return new SuccessResult();
        }

        public IResult CheckIfUserNameExist(string userName)
        {
            var result = _userService.GetByUserName(userName);
            if (result.Data != null)
            {
                return new ErrorResult(Messages.ThisUserNameAlreadyExists);
            }

            return new SuccessResult();
        }

        public IResult CheckIfUserNotExist(int userId)
        {
            var result = _userService.GetById(userId);
            if (result == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            return new SuccessResult();
        }
        public IResult CheckIfUserNameNotExist(string userName)
        {
            var result = _userService.GetByUserName(userName);
            if (result.Data == null)
            {
                return new ErrorResult(Messages.ThisUserNameAlreadyExists);
            }

            return new SuccessResult();
        }

        public IResult CheckIfUserNull(User user)
        {
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }

        public IResult CheckIfPasswordCorrect(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            return new SuccessResult();
        }

        public IResult CheckIfUserStatus(bool status)
        {
            if (!status)
            {
                return new ErrorResult(Messages.UserWasBlocked);
            }
            return new SuccessResult();
        }
    }
}