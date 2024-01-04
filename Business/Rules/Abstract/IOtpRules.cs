using System;
using Entities.Concrete;

namespace Business.Rules.Abstract;

public interface IOtpRules
{
    public void CheckIfOtpExpired(DateTime expirationDate);
    public void CheckIfOtpMatch(string otpFromDatabase, string otpToCheck);
    public void CheckIfUserHasOtp(string userName);
    public void CheckIfOtpNull(Otp otp);
    public void CheckIfOtpSentBefore(string email);

}