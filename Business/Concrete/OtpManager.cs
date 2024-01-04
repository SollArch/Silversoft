using System;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Mailing;
using Core.Utilities.Mailing;
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
        private readonly IAdminPasswordService _adminPasswordService;

        public OtpManager(IMailService mailService, IOtpDal otpDal, IUserService userService, IOtpRules otpRules, IAdminPasswordService adminPasswordService)
        {
            _mailService = mailService;
            _otpDal = otpDal;
            _userService = userService;
            _otpRules = otpRules;
            _adminPasswordService = adminPasswordService;
        }

        public IDataResult<User> CheckOtp(CheckOtpDto checkOtpDto)
        {
            var otp = GetByEmail(checkOtpDto.Email).Data;
            _otpRules.CheckIfOtpNull(otp);
            _otpRules.CheckIfOtpExpired(otp.ExpirationDate);
            _otpRules.CheckIfOtpMatch(otp.OneTimePassword, checkOtpDto.Otp);
            var userResult = _userService.GetByEmail(checkOtpDto.Email);
            _otpDal.Delete(otp);
            return userResult;
        }

        public IResult SendOtp(SendOtpDto sendOtpDto)
        {
            _otpRules.CheckIfOtpSentBefore(sendOtpDto.Email);
            var mail = new Mail
            {
                ToEmail = sendOtpDto.Email,
                Subject = "Doğrulama kodun işte burada!",
                TextBody = $"Seni burada görmek güzel, işte doğrulama kodun: {sendOtpDto.Otp}.\nBu kod 30 dakika geçerli olacak.",
                ToFullName = sendOtpDto.UserName,
            };
            var otp = new Otp
            {
                OtpId  = Guid.NewGuid(),
                Email = sendOtpDto.Email,
                OneTimePassword = sendOtpDto.Otp,
                ExpirationDate = DateTime.Now.AddMinutes(30)
            };
            _otpDal.Add(otp);
            _mailService.Send(mail,_adminPasswordService.GetPassword());
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
        private IDataResult<Otp> GetByEmail(string email)
        {
            return new SuccessDataResult<Otp>(_otpDal.Get(o => o.Email == email));
        }
    }
}