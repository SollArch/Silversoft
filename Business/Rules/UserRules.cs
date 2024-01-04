using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Entities.Concrete;
using Core.Exceptions;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTO.Post.Otp;

namespace Business.Rules
{
    public class UserRules : IUserRules
    {
        private readonly IUserDal _userDal;

        public UserRules(IUserDal userDal)
        {
            _userDal = userDal;
        }
        
        public void CheckIfStudentNumberExist(string studentNumber, Guid userId = default)
        {
            
            var result = _userDal.Get(u => u.StudentNumber.Equals(studentNumber));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisStudentNumberAlreadyExists);
            }
        }

        public void CheckIfEmailExist(string userEmail, Guid userId = default)
        {
            var result = _userDal.Get(u => u.Email.Equals(userEmail));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisEmailAlreadyExists);
            }
            
        }

        public void CheckIfUserNameExist(string userName, Guid userId = default)
        {
            var result = _userDal.Get( u => u.UserName.Equals(userName));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisUserNameAlreadyExists);
            }
        }

        public void CheckIfUserNotExist(Guid userId)
        {
            var result = _userDal.Get(u => u.UserId.Equals(userId));
            if (result == null)
            {
                throw new BusinessException(Messages.UserNotFound);
            }
        }


        public void CheckIfUserNull(User user)
        {
            if (user == null)
            {
                throw new BusinessException(Messages.UserNotFound);
            }
        }

        public void CheckIfPasswordCorrect(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                throw new BusinessException(Messages.PasswordError);
            }
        }

        public void CheckIfUserStatus(bool status)
        {
            if (!status)
            {
                throw new BusinessException(Messages.UserWasBlocked);
            }

        }

        public void CheckIfPasswordsSame(CheckOtpForChangePasswordDto checkOtpForChangePasswordDto)
        {
            if(checkOtpForChangePasswordDto.NewPassword == checkOtpForChangePasswordDto.CurrentPassword)
                throw new BusinessException(Messages.PasswordsSame);

        }

        public void CheckIfRequestForUpdate(User user)
        {
            var userFromDatabase = _userDal.Get(u => u.UserId.Equals(user.UserId));
            if (user.PasswordHash != null) return;
            user.PasswordHash = userFromDatabase.PasswordHash;
            user.PasswordSalt = userFromDatabase.PasswordSalt;
            user.Point = userFromDatabase.Point;
            user.Status = userFromDatabase.Status;
            user.Email = userFromDatabase.Email;
        }

        public void CheckIfUpdateUserIdEqualsRequestUserId(Guid userId, Guid requestUserId)
        {
            if (userId != requestUserId)
            {
                throw new BusinessException(Messages.AuthorizationDenied);
            }
        }
    }
}