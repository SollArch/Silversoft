using Business.Constants;
using Business.Rules.Abstract;
using Core.Entities.Concrete;
using Core.Exceptions;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTO.Post.Auth;

namespace Business.Rules
{
    public class UserRules : IUserRules
    {
        private readonly IUserDal _userDal;

        public UserRules(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public void CheckIfStudentNumberExist(string studentNumber, int userId = 0)
        {
            
            var result = _userDal.Get(u => u.StudentNumber.Equals(studentNumber));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisStudentNumberAlreadyExists);
            }
        }

        public void CheckIfEmailExist(string userEmail, int userId = 0)
        {
            var result = _userDal.Get(u => u.Email.Equals(userEmail));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisEmailAlreadyExists);
            }
            
        }

        public void CheckIfUserNameExist(string userName, int userId = 0)
        {
            var result = _userDal.Get( u => u.UserName.Equals(userName));
            if (result != null && result.UserId != userId)
            {
                throw new BusinessException(Messages.ThisUserNameAlreadyExists);
            }
        }

        public void CheckIfUserNotExist(int userId)
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

        public void CheckIfPasswordsSame(ChangePasswordDto changePasswordDto)
        {
            if(changePasswordDto.NewPassword == changePasswordDto.CurrentPassword)
                throw new BusinessException(Messages.PasswordsSame);

        }
    }
}