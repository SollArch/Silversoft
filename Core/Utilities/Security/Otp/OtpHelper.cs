using System;

namespace Core.Utilities.Security.Otp
{
    public class OtpHelper
    {
        public static string GenerateOtp()
        {
            const int otpLength = 6;
            
            Random random = new Random();
            int otpValue = random.Next((int)Math.Pow(10, otpLength - 1), (int)Math.Pow(10, otpLength));
            
            string otp = otpValue.ToString("D6");

            return otp;
        }
    }
}