using Core.Entities.Abstract;

namespace Entities.DTO.Post.Auth
{
    public class ForgotPasswordDto : IDto
    {
        public string Email { get; set; }
    }
}