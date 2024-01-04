using Core.Entities.Abstract;
using Entities.DTO.Post.Auth;

namespace Entities.DTO.Post.Otp;

public class CheckOtpForChangePasswordDto : IDto
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string Otp { get; set; }
}