using Core.Entities.Abstract;

namespace Entities.DTO
{
    public class ChangePasswordDto : IDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}