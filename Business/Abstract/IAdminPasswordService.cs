using Core.Utilities.Results;

namespace Business.Abstract;

public interface IAdminPasswordService
{
    IResult AddPassword(string password);
    string GetPassword();
}