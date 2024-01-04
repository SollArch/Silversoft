using System;
using Core.Entities.Concrete;
using Entities.DTO.Post.Auth;
using Entities.DTO.Post.Otp;

namespace Business.Rules.Abstract;

public interface IUserRules
{
    public void CheckIfStudentNumberExist(string studentNumber, Guid userId = default);
    public void CheckIfEmailExist(string userEmail, Guid userId = default);
    public void CheckIfUserNameExist(string userName, Guid userId = default);
    public void CheckIfUserNotExist(Guid userId);
    public void CheckIfUserNull(User user);
    public void CheckIfPasswordCorrect(string password, byte[] passwordHash, byte[] passwordSalt);
    public void CheckIfUserStatus(bool status);
    public void CheckIfPasswordsSame(CheckOtpForChangePasswordDto checkOtpForChangePasswordDto);
    public void CheckIfRequestForUpdate(User user);
    public void CheckIfUpdateUserIdEqualsRequestUserId(Guid userId, Guid requestUserId, bool fromForgetPassword = false);

}