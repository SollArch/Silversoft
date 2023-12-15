using Core.Utilities.Results;

namespace Business.Abstract;

public interface IPasswordService
{
    IResult AddPassword(string password);
    string GetPassword();
}