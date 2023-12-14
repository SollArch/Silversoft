using Core.Entities.Abstract;

namespace Entities.DTO.Post.Auth
{
    public class UserForLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}