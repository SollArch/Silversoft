using Core.Entities.Abstract;

namespace Entities.DTO.Post.Auth
{
    public class ChangePasswordDto : IDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}