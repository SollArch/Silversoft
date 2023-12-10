using System;
using Business.Abstract;
using Business.Constants;
using Business.Rules;
using Core.Entities.Concrete;
using Core.Mailing;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;

namespace Business.Concrete
{
    public class OtpManager : IOtpService
    {
        private readonly IOtpDal _otpDal;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public OtpManager(IMailService mailService, IOtpDal otpDal, IUserService userService)
        {
            _mailService = mailService;
            _otpDal = otpDal;
            _userService = userService;
        }

        public IDataResult<User> CheckOtp(CheckOtpDto checkOtpDto)
        {
            var otp = _otpDal.Get(x => x.UserName == checkOtpDto.UserName);
            var otpRules = new OtpRules(this);
            var result = BusinessRules.Run(
                otpRules.CheckIfOtpNull(otp),
                otpRules.CheckIfOtpExpired(otp),
                otpRules.CheckIfOtpMatch(otp, checkOtpDto.Otp)
                );
            if (result != null)
                return new ErrorDataResult<User>(result.Message);
            var user = _userService.GetByUserName(checkOtpDto.UserName);
            _otpDal.Delete(otp);
            return user;
        }

        public IResult SendOtp(SendOtpDto sendOtpDto)
        {
            var mail = new Mail
            {
                ToEmail = sendOtpDto.Email,
                Subject = "Your Otp Is Here",
                TextBody = $"Your otp is {sendOtpDto.Otp}. This OTP will expire in 30 minutes.",
                ToFullName = sendOtpDto.UserName,
            };
            var otp = new Otp
            {
                UserName = sendOtpDto.UserName,
                OneTimePassword = sendOtpDto.Otp,
                ExpirationDate = DateTime.Now.AddMinutes(30)
            };
            _otpDal.Add(otp);
            _mailService.Send(mail);
            return new SuccessResult(Messages.OtpSended);
        }
    }
}