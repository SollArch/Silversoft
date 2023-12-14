using Core.Entities.Abstract;

namespace Entities.DTO.Post.Otp
{
    public class CheckOtpDto : IDto
    {
        public string UserName { get; set; }
        public string Otp { get; set; }
    }
}