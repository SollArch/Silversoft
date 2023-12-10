using Core.Entities.Abstract;

namespace Entities.DTO
{
    public class ForgotPasswordDto : IDto
    {
        public string Email { get; set; }
    }
}