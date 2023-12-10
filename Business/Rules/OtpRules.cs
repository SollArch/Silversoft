using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Rules
{
    public class OtpRules
    {
        private readonly IOtpService _otpService;

        public OtpRules(IOtpService otpService)
        {
            _otpService = otpService;
        }

        public IResult CheckIfOtpNull(Otp otp)
        {
            if (otp == null)
            {
                return new ErrorResult(Messages.OtpNotFound);
            }

            return new SuccessResult();
        }
        
        public IResult CheckIfOtpExpired(Otp otp)
        {
            if (otp.ExpirationDate < System.DateTime.Now)
            {
                return new ErrorResult(Messages.OtpExpired);
            }

            return new SuccessResult();
        }
        
        public IResult CheckIfOtpMatch(Otp otp, string otpToCheck)
        {
            if (otp.OneTimePassword != otpToCheck)
            {
                return new ErrorResult(Messages.OtpNotMatch);
            }

            return new SuccessResult();
        }
    }
}