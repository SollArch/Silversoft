using Core.Entities.Concrete;
using Entities.DTO.Post.Auth;

namespace Business.Rules.Abstract;

public interface IUserRules
{
    public void CheckIfStudentNumberExist(string studentNumber, int userId = 0);
    public void CheckIfEmailExist(string userEmail, int userId = 0);
    public void CheckIfUserNameExist(string userName, int userId = 0);
    public void CheckIfUserNotExist(int userId);
    public void CheckIfUserNull(User user);
    public void CheckIfPasswordCorrect(string password, byte[] passwordHash, byte[] passwordSalt);
    public void CheckIfUserStatus(bool status);
    public void CheckIfPasswordsSame(ChangePasswordDto changePasswordDto);

}