using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Rules
{
    public class OtpRules : IOtpRules
    {
        private readonly IOtpDal _otpDal;

        public OtpRules(IOtpDal otpDal)
        {
            _otpDal = otpDal;
        }

        public void CheckIfOtpExpired(DateTime expirationDate)
        {
            if (expirationDate >= DateTime.Now) return;
            throw new BusinessException(Messages.OtpExpired);

        }
        
        public void CheckIfOtpMatch(string otpFromDatabase, string otpToCheck)
        {
            if (otpFromDatabase != otpToCheck)
            {
                throw new BusinessException(Messages.OtpNotMatch);
            }

        }
        
        public void CheckIfUserHasOtp(string userName)
        {
            var otp = _otpDal.Get(o => o.UserName.Equals(userName));
            if (otp != null)
            {
                throw new BusinessException(Messages.UserHasOtp);
            }

        }

        public void CheckIfOtpNull(Otp otp)
        {
            if(otp == null)
                throw new BusinessException(Messages.OtpNotFound);
        }
    }
}