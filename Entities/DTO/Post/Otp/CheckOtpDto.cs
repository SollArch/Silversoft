using Core.Entities.Abstract;

namespace Entities.DTO.Post.Otp
{
    public class CheckOtpDto : IDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}