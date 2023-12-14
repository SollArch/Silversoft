using System;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Mailing;
using Core.Utilities.Results;
using Core.Utilities.Security.Otp;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.Otp;

namespace Business.Concrete
{
    public class OtpManager : IOtpService
    {
        private readonly IOtpDal _otpDal;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly IOtpRules _otpRules;

        public OtpManager(IMailService mailService, IOtpDal otpDal, IUserService userService, IOtpRules otpRules)
        {
            _mailService = mailService;
            _otpDal = otpDal;
            _userService = userService;
            _otpRules = otpRules;
        }

        public IDataResult<User> CheckOtp(CheckOtpDto checkOtpDto)
        {
            var otp = GetByUserName(checkOtpDto.UserName).Data;
            _otpRules.CheckIfOtpNull(otp);
            _otpRules.CheckIfOtpExpired(otp.ExpirationDate);
            _otpRules.CheckIfOtpMatch(otp.OneTimePassword, checkOtpDto.Otp);
            var userResult = _userService.GetByUserName(checkOtpDto.UserName);
            _otpDal.Delete(otp);
            return userResult;
        }

        public IResult SendOtp(SendOtpDto sendOtpDto)
        {
            var mail = new Mail
            {
                ToEmail = sendOtpDto.Email,
                Subject = "Doğrulama kodun işte burada!",
                TextBody = $"Seni burada görmek güzel, işte doğrulama kodun: {sendOtpDto.Otp}.\nBu kod 30 dakika geçerli olacak.",
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

        public IDataResult<string> GenerateOtp()
        {
            return new SuccessDataResult<string>(data: OtpHelper.GenerateOtp());
        }
        
        [CacheRemoveAspect("IOtpService.Get")]
        public IResult Delete(Otp otp)
        {
            _otpDal.Delete(otp);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<Otp> GetByUserName(string userName)
        {
            return new SuccessDataResult<Otp>(_otpDal.Get(o => o.UserName == userName));
        }
    }
}