using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTO;

namespace Business.Abstract
{
    public interface IOtpService
    {
        IDataResult<User> CheckOtp(CheckOtpDto checkOtpDto);
        IResult SendOtp(SendOtpDto sendOtpDto);
        
    }
}