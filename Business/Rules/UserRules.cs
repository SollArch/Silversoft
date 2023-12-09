using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Rules
{
    public abstract class UserRules
    {
        private static IUserDal _userDal;

        protected UserRules(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public static IResult CheckIfStudentNumberExist(string studentNumber)
        {
            
            var result = _userDal.Get(u => u.StudentNumber == studentNumber);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisStudentNumberAlreadyExists);
            }

            return new SuccessResult();
        }

        public static IResult CheckIfEmailExist(string userEmail)
        {
            var result = _userDal.Get(u => u.Email == userEmail);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisEmailAlreadyExists);
            }

            return new SuccessResult();
        }

        public static IResult CheckIfUserNameExist(string userName)
        {
            var result = _userDal.Get(u => u.UserName == userName);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisUserNameAlreadyExists);
            }

            return new SuccessResult();
        }

        public static IResult CheckIfUserNotExist(int userId)
        {
            var result = _userDal.Get(u => u.UserId == userId);
            if (result == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            return new SuccessResult();
        }
    }
}