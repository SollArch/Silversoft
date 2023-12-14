using Core.Entities.Abstract;

namespace Entities.DTO.Post.Otp
{
    public class SendOtpDto : IDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Otp { get; set; }
    }
}