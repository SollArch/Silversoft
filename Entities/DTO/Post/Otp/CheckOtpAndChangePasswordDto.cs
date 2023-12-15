using Core.Entities.Abstract;
using Entities.DTO.Post.Auth;

namespace Entities.DTO.Post.Otp;

public class CheckOtpAndChangePasswordDto : IDto
{
    public CheckOtpDto CheckOtpDto { get; set; }
    public ChangePasswordDto ChangePasswordDto { get; set; }
}